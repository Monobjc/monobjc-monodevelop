using System;
using Antlr4.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace MonobjcDevelop.Monobjc.Parsing
{
	public class NativeClassDescriptionCollector<Result> : ObjCBaseVisitor<Result>
	{
		private const String TYPE_IBOUTLET = "IBOutlet";
		private const String TYPE_IBACTION = "IBAction";
		private const String TYPE_VOID = "void";

		public IList<NativeClassDescriptor> classDescriptors = new List<NativeClassDescriptor> ();
		public Stack<Object> objects = new Stack<Object> ();
		public String instanceVariables = null;

		public NativeClassDescriptor[] Descriptors {
			get { return this.classDescriptors.ToArray (); }
		}

		public override Result VisitClass_interface (ObjCParser.Class_interfaceContext context)
		{
			NativeClassDescriptor classDescriptor = new NativeClassDescriptor ();
			this.objects.Push (classDescriptor);
			Result result = base.VisitClass_interface (context);
			this.objects.Pop ();
			this.classDescriptors.Add (classDescriptor);
			return result;
		}

		public override Result VisitClass_name (ObjCParser.Class_nameContext context)
		{
			NativeClassDescriptor classDescriptor = this.GetObject<NativeClassDescriptor> ();
			if (classDescriptor != null) {
				classDescriptor.ClassName = classDescriptor.ClassName ?? context.GetText ();
			}
			return base.VisitClass_name (context);
		}

		public override Result VisitSuperclass_name (ObjCParser.Superclass_nameContext context)
		{
			NativeClassDescriptor classDescriptor = this.GetObject<NativeClassDescriptor> ();
			if (classDescriptor != null) {
				classDescriptor.SuperClassName = classDescriptor.SuperClassName ?? context.GetText ();
			}
			return base.VisitSuperclass_name (context);
		}

		public override Result VisitInstance_variables (ObjCParser.Instance_variablesContext context)
		{
			this.instanceVariables = context.GetText ();
			Result result = base.VisitInstance_variables (context);
			this.instanceVariables = null;
			return result;
		}

		public override Result VisitStruct_declaration (ObjCParser.Struct_declarationContext context)
		{
			if (!String.IsNullOrEmpty (this.instanceVariables)) {
				NativeInstanceVariableDescriptor instanceVariableDescriptor = new NativeInstanceVariableDescriptor ();

				// Store the whole declaration in the type for later processing
				instanceVariableDescriptor.Type = context.GetText ();
				this.objects.Push (instanceVariableDescriptor);
			}
			Result result = base.VisitStruct_declaration (context);

			// If instance variable parsing is in progress, then add any one found
			if (!String.IsNullOrEmpty (this.instanceVariables)) {
				NativeInstanceVariableDescriptor instanceVariableDescriptor = GetObject<NativeInstanceVariableDescriptor> (true);
				if (instanceVariableDescriptor != null) {
					NativeClassDescriptor classDescriptor = this.GetObject<NativeClassDescriptor> (); 
					if (classDescriptor != null) {
						classDescriptor.InstanceVariables.Add (instanceVariableDescriptor);
					}
				}
			}
			return result;
		}

		public override Result VisitDeclarator (ObjCParser.DeclaratorContext context)
		{
			NativeInstanceVariableDescriptor instanceVariableDescriptor = this.GetObject<NativeInstanceVariableDescriptor> ();
			if (instanceVariableDescriptor != null) {
				String text = context.GetText ();
				String type = instanceVariableDescriptor.Type;

				// Check if declarator is contained within the type
				// - on the first pass, the type is the whole declaration
				// - on subsequent passes, the type has been trimmed
				int pos = type.IndexOf (text);
				if (pos != -1) {
					instanceVariableDescriptor.Type = type.Substring (0, pos);
					instanceVariableDescriptor.Name = context.GetText ();
				}

				// Check if the type is an outlet
				if (type.StartsWith (TYPE_IBOUTLET, StringComparison.OrdinalIgnoreCase)) {
					instanceVariableDescriptor.IBOutlet = true;
					instanceVariableDescriptor.Type = instanceVariableDescriptor.Type.Substring (TYPE_IBOUTLET.Length);
				}

				// Trim down the name on subsequent passes
				pos = (instanceVariableDescriptor.Name ?? String.Empty).IndexOf (text);
				if (pos != -1) {
					instanceVariableDescriptor.Name = text;
				}
			}
			Result result = base.VisitDeclarator (context);
			return result;
		}

		public override Result VisitClass_method_declaration (ObjCParser.Class_method_declarationContext context)
		{
			// Push a new class method
			NativeMethodDescriptor methodDescriptor = new NativeMethodDescriptor ();
			methodDescriptor.Selector = String.Empty;
			methodDescriptor.Parameters = new List<NativeMethodDescriptor.Parameter> ();
			methodDescriptor.Static = true;

			this.objects.Push (methodDescriptor);
			Result result = base.VisitClass_method_declaration (context);
			this.objects.Pop ();

			// Add the method to the class
			NativeClassDescriptor classDescriptor = this.GetObject<NativeClassDescriptor> (); 
			if (classDescriptor != null) {
				classDescriptor.Methods.Add (methodDescriptor);
			}
			return result;
		}

		public override Result VisitInstance_method_declaration (ObjCParser.Instance_method_declarationContext context)
		{
			// Push a new instance method
			NativeMethodDescriptor methodDescriptor = new NativeMethodDescriptor ();
			methodDescriptor.Selector = String.Empty;
			methodDescriptor.Parameters = new List<NativeMethodDescriptor.Parameter> ();
			methodDescriptor.Static = false;

			this.objects.Push (methodDescriptor);
			Result result = base.VisitInstance_method_declaration (context);
			this.objects.Pop ();

			// Add the method to the class
			NativeClassDescriptor classDescriptor = this.GetObject<NativeClassDescriptor> (); 
			if (classDescriptor != null) {
				classDescriptor.Methods.Add (methodDescriptor);
			}
			return result;
		}

		public override Result VisitMethod_selector (ObjCParser.Method_selectorContext context)
		{
			// Store the method signature
			NativeMethodDescriptor methodDescriptor = this.GetObject<NativeMethodDescriptor> ();
			if (methodDescriptor != null) {
				methodDescriptor.Selector = context.GetText ();
			}
			Result result = base.VisitMethod_selector (context);
			return result;
		}

		public override Result VisitKeyword_declarator (ObjCParser.Keyword_declaratorContext context)
		{
			// Parse the declarator (part of the method signature)
			String text = context.GetText ();
			NativeMethodDescriptor methodDescriptor = this.GetObject<NativeMethodDescriptor> ();
			if (methodDescriptor != null) {
				String part = text;
				int pos = text.IndexOf (":");
				if (pos >= 0) {
					part = text.Substring (0, pos + 1);
				}
				methodDescriptor.Selector = methodDescriptor.Selector.Replace (text, part);
			}

			Result result = base.VisitKeyword_declarator (context);

			// Retrieve the name of the last parsed parameter
			if (methodDescriptor != null && methodDescriptor.Parameters.Count > 0) {
				NativeMethodDescriptor.Parameter parameter = methodDescriptor.Parameters.Last ();
				int pos = text.IndexOf (parameter.Type);
				if (pos >= 0) {
					parameter.Name = text.Substring (pos + parameter.Type.Length).Trim (new [] { '(', ')' });
				}
			}
			return result;
		}

		public override Result VisitType_name (ObjCParser.Type_nameContext context)
		{
			NativeMethodDescriptor methodDescriptor = this.GetObject<NativeMethodDescriptor> ();
			if (methodDescriptor != null) {
				String type = context.GetText ();

				// The first type collected is the return type
				if (String.IsNullOrEmpty (methodDescriptor.ReturnType)) {
					methodDescriptor.ReturnType = type;
					if (String.Equals (type, TYPE_IBACTION, StringComparison.OrdinalIgnoreCase)) {
						methodDescriptor.IBAction = true;
						methodDescriptor.ReturnType = TYPE_VOID;
					}
				} else {
					// The subsequent are parameter types
					NativeMethodDescriptor.Parameter parameter = new NativeMethodDescriptor.Parameter () { Type=type };
					methodDescriptor.Parameters.Add (parameter);
				}
			}
			Result result = base.VisitType_name (context);
			return result;
		}

		private T GetObject<T> (bool remove = false) where T : class
		{
			if (this.objects.Count == 0) {
				return default(T);
			}
			T head = this.objects.Peek () as T;
			if (head != null && remove) {
				this.objects.Pop ();
			}
			return head;
		}
	}
}

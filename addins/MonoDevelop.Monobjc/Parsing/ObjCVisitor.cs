// Generated from ObjC.g4 by ANTLR 4.0.1-SNAPSHOT
namespace MonobjcDevelop.Monobjc.Parsing {
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

public interface IObjCVisitor<Result> : IParseTreeVisitor<Result> {
	Result VisitSynchronized_statement(ObjCParser.Synchronized_statementContext context);

	Result VisitTry_statement(ObjCParser.Try_statementContext context);

	Result VisitDeclarator(ObjCParser.DeclaratorContext context);

	Result VisitReceiver(ObjCParser.ReceiverContext context);

	Result VisitProperty_implementation(ObjCParser.Property_implementationContext context);

	Result VisitException_declarator(ObjCParser.Exception_declaratorContext context);

	Result VisitLabeled_statement(ObjCParser.Labeled_statementContext context);

	Result VisitMessage_expression(ObjCParser.Message_expressionContext context);

	Result VisitCompound_statement(ObjCParser.Compound_statementContext context);

	Result VisitCast_expression(ObjCParser.Cast_expressionContext context);

	Result VisitEquality_expression(ObjCParser.Equality_expressionContext context);

	Result VisitStorage_class_specifier(ObjCParser.Storage_class_specifierContext context);

	Result VisitClass_interface(ObjCParser.Class_interfaceContext context);

	Result VisitFunction_definition(ObjCParser.Function_definitionContext context);

	Result VisitTranslation_unit(ObjCParser.Translation_unitContext context);

	Result VisitType_qualifier(ObjCParser.Type_qualifierContext context);

	Result VisitProtocol_list(ObjCParser.Protocol_listContext context);

	Result VisitLogical_and_expression(ObjCParser.Logical_and_expressionContext context);

	Result VisitShift_expression(ObjCParser.Shift_expressionContext context);

	Result VisitMethod_declaration(ObjCParser.Method_declarationContext context);

	Result VisitType_name(ObjCParser.Type_nameContext context);

	Result VisitMethod_definition(ObjCParser.Method_definitionContext context);

	Result VisitParameter_list(ObjCParser.Parameter_listContext context);

	Result VisitEncode_expression(ObjCParser.Encode_expressionContext context);

	Result VisitInclusive_or_expression(ObjCParser.Inclusive_or_expressionContext context);

	Result VisitConstant_expression(ObjCParser.Constant_expressionContext context);

	Result VisitRelational_expression(ObjCParser.Relational_expressionContext context);

	Result VisitClass_name(ObjCParser.Class_nameContext context);

	Result VisitDeclaration_specifiers(ObjCParser.Declaration_specifiersContext context);

	Result VisitParameter_declaration_list(ObjCParser.Parameter_declaration_listContext context);

	Result VisitPostfix_expression(ObjCParser.Postfix_expressionContext context);

	Result VisitMessage_selector(ObjCParser.Message_selectorContext context);

	Result VisitKeyword_declarator(ObjCParser.Keyword_declaratorContext context);

	Result VisitProperty_declaration(ObjCParser.Property_declarationContext context);

	Result VisitPreprocessor_declaration(ObjCParser.Preprocessor_declarationContext context);

	Result VisitProperty_attributes_declaration(ObjCParser.Property_attributes_declarationContext context);

	Result VisitSelection_statement(ObjCParser.Selection_statementContext context);

	Result VisitProtocol_reference_list(ObjCParser.Protocol_reference_listContext context);

	Result VisitFinally_statement(ObjCParser.Finally_statementContext context);

	Result VisitTry_block(ObjCParser.Try_blockContext context);

	Result VisitClass_method_definition(ObjCParser.Class_method_definitionContext context);

	Result VisitProtocol_declaration_list(ObjCParser.Protocol_declaration_listContext context);

	Result VisitProperty_synthesize_list(ObjCParser.Property_synthesize_listContext context);

	Result VisitClass_declaration_list(ObjCParser.Class_declaration_listContext context);

	Result VisitConditional_expression(ObjCParser.Conditional_expressionContext context);

	Result VisitPrimary_expression(ObjCParser.Primary_expressionContext context);

	Result VisitKeyword_argument(ObjCParser.Keyword_argumentContext context);

	Result VisitSpecifier_qualifier_list(ObjCParser.Specifier_qualifier_listContext context);

	Result VisitVisibility_specification(ObjCParser.Visibility_specificationContext context);

	Result VisitCategory_interface(ObjCParser.Category_interfaceContext context);

	Result VisitInitializer(ObjCParser.InitializerContext context);

	Result VisitSelector(ObjCParser.SelectorContext context);

	Result VisitProtocol_declaration(ObjCParser.Protocol_declarationContext context);

	Result VisitExpression(ObjCParser.ExpressionContext context);

	Result VisitProperty_attributes_list(ObjCParser.Property_attributes_listContext context);

	Result VisitAssignment_expression(ObjCParser.Assignment_expressionContext context);

	Result VisitMultiplicative_expression(ObjCParser.Multiplicative_expressionContext context);

	Result VisitProperty_attribute(ObjCParser.Property_attributeContext context);

	Result VisitJump_statement(ObjCParser.Jump_statementContext context);

	Result VisitInstance_method_definition(ObjCParser.Instance_method_definitionContext context);

	Result VisitEnumerator(ObjCParser.EnumeratorContext context);

	Result VisitProtocol_qualifier(ObjCParser.Protocol_qualifierContext context);

	Result VisitStruct_declarator_list(ObjCParser.Struct_declarator_listContext context);

	Result VisitInterface_declaration_list(ObjCParser.Interface_declaration_listContext context);

	Result VisitStruct_declarator(ObjCParser.Struct_declaratorContext context);

	Result VisitProtocol_expression(ObjCParser.Protocol_expressionContext context);

	Result VisitClass_method_declaration(ObjCParser.Class_method_declarationContext context);

	Result VisitSelector_name(ObjCParser.Selector_nameContext context);

	Result VisitDeclaration(ObjCParser.DeclarationContext context);

	Result VisitInit_declarator_list(ObjCParser.Init_declarator_listContext context);

	Result VisitInit_declarator(ObjCParser.Init_declaratorContext context);

	Result VisitStruct_or_union_specifier(ObjCParser.Struct_or_union_specifierContext context);

	Result VisitExclusive_or_expression(ObjCParser.Exclusive_or_expressionContext context);

	Result VisitEnumerator_list(ObjCParser.Enumerator_listContext context);

	Result VisitStatement(ObjCParser.StatementContext context);

	Result VisitAdditive_expression(ObjCParser.Additive_expressionContext context);

	Result VisitUnary_operator(ObjCParser.Unary_operatorContext context);

	Result VisitCategory_name(ObjCParser.Category_nameContext context);

	Result VisitInstance_variables(ObjCParser.Instance_variablesContext context);

	Result VisitIteration_statement(ObjCParser.Iteration_statementContext context);

	Result VisitLogical_or_expression(ObjCParser.Logical_or_expressionContext context);

	Result VisitThrow_statement(ObjCParser.Throw_statementContext context);

	Result VisitIdentifier(ObjCParser.IdentifierContext context);

	Result VisitArgument_expression_list(ObjCParser.Argument_expression_listContext context);

	Result VisitClass_implementation(ObjCParser.Class_implementationContext context);

	Result VisitStruct_declaration(ObjCParser.Struct_declarationContext context);

	Result VisitEnum_specifier(ObjCParser.Enum_specifierContext context);

	Result VisitParameter_declaration(ObjCParser.Parameter_declarationContext context);

	Result VisitMethod_selector(ObjCParser.Method_selectorContext context);

	Result VisitAssignment_operator(ObjCParser.Assignment_operatorContext context);

	Result VisitUnary_expression(ObjCParser.Unary_expressionContext context);

	Result VisitInstance_method_declaration(ObjCParser.Instance_method_declarationContext context);

	Result VisitMethod_type(ObjCParser.Method_typeContext context);

	Result VisitProtocol_name(ObjCParser.Protocol_nameContext context);

	Result VisitCategory_implementation(ObjCParser.Category_implementationContext context);

	Result VisitConstant(ObjCParser.ConstantContext context);

	Result VisitStatement_list(ObjCParser.Statement_listContext context);

	Result VisitAbstract_declarator(ObjCParser.Abstract_declaratorContext context);

	Result VisitExternal_declaration(ObjCParser.External_declarationContext context);

	Result VisitAbstract_declarator_suffix(ObjCParser.Abstract_declarator_suffixContext context);

	Result VisitClass_list(ObjCParser.Class_listContext context);

	Result VisitProperty_synthesize_item(ObjCParser.Property_synthesize_itemContext context);

	Result VisitDirect_declarator(ObjCParser.Direct_declaratorContext context);

	Result VisitImplementation_definition_list(ObjCParser.Implementation_definition_listContext context);

	Result VisitAnd_expression(ObjCParser.And_expressionContext context);

	Result VisitMacro_specification(ObjCParser.Macro_specificationContext context);

	Result VisitSuperclass_name(ObjCParser.Superclass_nameContext context);

	Result VisitCatch_statement(ObjCParser.Catch_statementContext context);

	Result VisitType_specifier(ObjCParser.Type_specifierContext context);

	Result VisitSelector_expression(ObjCParser.Selector_expressionContext context);

	Result VisitDeclarator_suffix(ObjCParser.Declarator_suffixContext context);
}
} // namespace TestObjC

// Generated from ObjC.g4 by ANTLR 4.0.1-SNAPSHOT
namespace MonobjcDevelop.Monobjc.Parsing {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using DFA = Antlr4.Runtime.Dfa.DFA;

public partial class ObjCParser : Parser {
	public const int
		T__112=1, T__111=2, T__110=3, T__109=4, T__108=5, T__107=6, T__106=7, 
		T__105=8, T__104=9, T__103=10, T__102=11, T__101=12, T__100=13, T__99=14, 
		T__98=15, T__97=16, T__96=17, T__95=18, T__94=19, T__93=20, T__92=21, 
		T__91=22, T__90=23, T__89=24, T__88=25, T__87=26, T__86=27, T__85=28, 
		T__84=29, T__83=30, T__82=31, T__81=32, T__80=33, T__79=34, T__78=35, 
		T__77=36, T__76=37, T__75=38, T__74=39, T__73=40, T__72=41, T__71=42, 
		T__70=43, T__69=44, T__68=45, T__67=46, T__66=47, T__65=48, T__64=49, 
		T__63=50, T__62=51, T__61=52, T__60=53, T__59=54, T__58=55, T__57=56, 
		T__56=57, T__55=58, T__54=59, T__53=60, T__52=61, T__51=62, T__50=63, 
		T__49=64, T__48=65, T__47=66, T__46=67, T__45=68, T__44=69, T__43=70, 
		T__42=71, T__41=72, T__40=73, T__39=74, T__38=75, T__37=76, T__36=77, 
		T__35=78, T__34=79, T__33=80, T__32=81, T__31=82, T__30=83, T__29=84, 
		T__28=85, T__27=86, T__26=87, T__25=88, T__24=89, T__23=90, T__22=91, 
		T__21=92, T__20=93, T__19=94, T__18=95, T__17=96, T__16=97, T__15=98, 
		T__14=99, T__13=100, T__12=101, T__11=102, T__10=103, T__9=104, T__8=105, 
		T__7=106, T__6=107, T__5=108, T__4=109, T__3=110, T__2=111, T__1=112, 
		T__0=113, IDENTIFIER=114, CHARACTER_LITERAL=115, STRING_LITERAL=116, HEX_LITERAL=117, 
		DECIMAL_LITERAL=118, OCTAL_LITERAL=119, FLOATING_POINT_LITERAL=120, IMPORT=121, 
		INCLUDE=122, PRAGMA=123, WS=124, COMMENT=125, LINE_COMMENT=126;
	public static readonly string[] tokenNames = {
		"<INVALID>", "'self'", "'register'", "'*'", "'@synchronized'", "'double'", 
		"'inout'", "'}'", "'float'", "'char'", "'do'", "'auto'", "'*='", "'oneway'", 
		"')'", "'@trystatement'", "'unsigned'", "'@implementation'", "'goto'", 
		"'@property'", "'byref'", "'#ifdef'", "'@encode'", "'|'", "'!'", "'long'", 
		"'sizeof'", "'@protected'", "'short'", "'-='", "'in'", "','", "'while'", 
		"'@finally'", "'-'", "'if'", "'int'", "'?'", "'void'", "'bycopy'", "'>>='", 
		"'...'", "'break'", "'+='", "'^='", "'else'", "'.+'", "'struct'", "'++'", 
		"'extern'", "'.'", "'+'", "'&&'", "'||'", "'>'", "'%='", "'/='", "'switch'", 
		"'/'", "'~'", "'out'", "'@throw'", "'&'", "'['", "'#ifndef'", "'@end'", 
		"'<'", "'--'", "'continue'", "'!='", "'<='", "'id'", "'<<'", "'@selector'", 
		"'@protocol'", "'case'", "'@package'", "'@synthesize'", "'@dynamic'", 
		"'super'", "'%'", "'->'", "'union'", "'signed'", "'@catch'", "'='", "'const'", 
		"'|='", "'enum'", "'@class'", "'<<='", "']'", "'default'", "'@public'", 
		"':'", "'('", "'&='", "'#endif'", "'{'", "'#undef'", "'static'", "'#define'", 
		"'>>'", "'^'", "'@private'", "'for'", "'return'", "'typedef'", "';'", 
		"'volatile'", "'@interface'", "'=='", "'#if'", "'>='", "IDENTIFIER", "CHARACTER_LITERAL", 
		"STRING_LITERAL", "HEX_LITERAL", "DECIMAL_LITERAL", "OCTAL_LITERAL", "FLOATING_POINT_LITERAL", 
		"IMPORT", "INCLUDE", "PRAGMA", "WS", "COMMENT", "LINE_COMMENT"
	};
	public const int
		RULE_translation_unit = 0, RULE_external_declaration = 1, RULE_preprocessor_declaration = 2, 
		RULE_macro_specification = 3, RULE_class_interface = 4, RULE_category_interface = 5, 
		RULE_class_implementation = 6, RULE_category_implementation = 7, RULE_protocol_declaration = 8, 
		RULE_protocol_declaration_list = 9, RULE_class_declaration_list = 10, 
		RULE_class_list = 11, RULE_protocol_reference_list = 12, RULE_protocol_list = 13, 
		RULE_property_declaration = 14, RULE_property_attributes_declaration = 15, 
		RULE_property_attributes_list = 16, RULE_property_attribute = 17, RULE_class_name = 18, 
		RULE_superclass_name = 19, RULE_category_name = 20, RULE_protocol_name = 21, 
		RULE_instance_variables = 22, RULE_visibility_specification = 23, RULE_interface_declaration_list = 24, 
		RULE_class_method_declaration = 25, RULE_instance_method_declaration = 26, 
		RULE_method_declaration = 27, RULE_implementation_definition_list = 28, 
		RULE_class_method_definition = 29, RULE_instance_method_definition = 30, 
		RULE_method_definition = 31, RULE_method_selector = 32, RULE_keyword_declarator = 33, 
		RULE_selector = 34, RULE_method_type = 35, RULE_property_implementation = 36, 
		RULE_property_synthesize_list = 37, RULE_property_synthesize_item = 38, 
		RULE_type_specifier = 39, RULE_type_qualifier = 40, RULE_protocol_qualifier = 41, 
		RULE_primary_expression = 42, RULE_message_expression = 43, RULE_receiver = 44, 
		RULE_message_selector = 45, RULE_keyword_argument = 46, RULE_selector_expression = 47, 
		RULE_selector_name = 48, RULE_protocol_expression = 49, RULE_encode_expression = 50, 
		RULE_exception_declarator = 51, RULE_try_statement = 52, RULE_catch_statement = 53, 
		RULE_finally_statement = 54, RULE_throw_statement = 55, RULE_try_block = 56, 
		RULE_synchronized_statement = 57, RULE_function_definition = 58, RULE_declaration = 59, 
		RULE_declaration_specifiers = 60, RULE_storage_class_specifier = 61, RULE_init_declarator_list = 62, 
		RULE_init_declarator = 63, RULE_struct_or_union_specifier = 64, RULE_struct_declaration = 65, 
		RULE_specifier_qualifier_list = 66, RULE_struct_declarator_list = 67, 
		RULE_struct_declarator = 68, RULE_enum_specifier = 69, RULE_enumerator_list = 70, 
		RULE_enumerator = 71, RULE_declarator = 72, RULE_direct_declarator = 73, 
		RULE_declarator_suffix = 74, RULE_parameter_list = 75, RULE_parameter_declaration = 76, 
		RULE_initializer = 77, RULE_type_name = 78, RULE_abstract_declarator = 79, 
		RULE_abstract_declarator_suffix = 80, RULE_parameter_declaration_list = 81, 
		RULE_statement_list = 82, RULE_statement = 83, RULE_labeled_statement = 84, 
		RULE_compound_statement = 85, RULE_selection_statement = 86, RULE_iteration_statement = 87, 
		RULE_jump_statement = 88, RULE_expression = 89, RULE_assignment_expression = 90, 
		RULE_assignment_operator = 91, RULE_conditional_expression = 92, RULE_constant_expression = 93, 
		RULE_logical_or_expression = 94, RULE_logical_and_expression = 95, RULE_inclusive_or_expression = 96, 
		RULE_exclusive_or_expression = 97, RULE_and_expression = 98, RULE_equality_expression = 99, 
		RULE_relational_expression = 100, RULE_shift_expression = 101, RULE_additive_expression = 102, 
		RULE_multiplicative_expression = 103, RULE_cast_expression = 104, RULE_unary_expression = 105, 
		RULE_unary_operator = 106, RULE_postfix_expression = 107, RULE_argument_expression_list = 108, 
		RULE_identifier = 109, RULE_constant = 110;
	public static readonly string[] ruleNames = {
		"translation_unit", "external_declaration", "preprocessor_declaration", 
		"macro_specification", "class_interface", "category_interface", "class_implementation", 
		"category_implementation", "protocol_declaration", "protocol_declaration_list", 
		"class_declaration_list", "class_list", "protocol_reference_list", "protocol_list", 
		"property_declaration", "property_attributes_declaration", "property_attributes_list", 
		"property_attribute", "class_name", "superclass_name", "category_name", 
		"protocol_name", "instance_variables", "visibility_specification", "interface_declaration_list", 
		"class_method_declaration", "instance_method_declaration", "method_declaration", 
		"implementation_definition_list", "class_method_definition", "instance_method_definition", 
		"method_definition", "method_selector", "keyword_declarator", "selector", 
		"method_type", "property_implementation", "property_synthesize_list", 
		"property_synthesize_item", "type_specifier", "type_qualifier", "protocol_qualifier", 
		"primary_expression", "message_expression", "receiver", "message_selector", 
		"keyword_argument", "selector_expression", "selector_name", "protocol_expression", 
		"encode_expression", "exception_declarator", "try_statement", "catch_statement", 
		"finally_statement", "throw_statement", "try_block", "synchronized_statement", 
		"function_definition", "declaration", "declaration_specifiers", "storage_class_specifier", 
		"init_declarator_list", "init_declarator", "struct_or_union_specifier", 
		"struct_declaration", "specifier_qualifier_list", "struct_declarator_list", 
		"struct_declarator", "enum_specifier", "enumerator_list", "enumerator", 
		"declarator", "direct_declarator", "declarator_suffix", "parameter_list", 
		"parameter_declaration", "initializer", "type_name", "abstract_declarator", 
		"abstract_declarator_suffix", "parameter_declaration_list", "statement_list", 
		"statement", "labeled_statement", "compound_statement", "selection_statement", 
		"iteration_statement", "jump_statement", "expression", "assignment_expression", 
		"assignment_operator", "conditional_expression", "constant_expression", 
		"logical_or_expression", "logical_and_expression", "inclusive_or_expression", 
		"exclusive_or_expression", "and_expression", "equality_expression", "relational_expression", 
		"shift_expression", "additive_expression", "multiplicative_expression", 
		"cast_expression", "unary_expression", "unary_operator", "postfix_expression", 
		"argument_expression_list", "identifier", "constant"
	};

	public override string GrammarFileName { get { return "ObjC.g4"; } }

	public override string[] TokenNames { get { return tokenNames; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public ObjCParser(ITokenStream input)
		: base(input)
	{
		_interp = new ParserATNSimulator(this,_ATN);
	}
	public partial class Translation_unitContext : ParserRuleContext {
		public External_declarationContext[] external_declaration() {
			return GetRuleContexts<External_declarationContext>();
		}
		public ITerminalNode EOF() { return GetToken(ObjCParser.Eof, 0); }
		public External_declarationContext external_declaration(int i) {
			return GetRuleContext<External_declarationContext>(i);
		}
		public Translation_unitContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_translation_unit; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterTranslation_unit(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitTranslation_unit(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitTranslation_unit(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Translation_unitContext translation_unit() {
		Translation_unitContext _localctx = new Translation_unitContext(_ctx, State);
		EnterRule(_localctx, 0, RULE_translation_unit);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 223;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				{
				State = 222; external_declaration();
				}
				}
				State = 225;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 3) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 17) | (1L << 20) | (1L << 21) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 60))) != 0) || ((((_la - 64)) & ~0x3f) == 0 && ((1L << (_la - 64)) & ((1L << (64 - 64)) | (1L << (71 - 64)) | (1L << (74 - 64)) | (1L << (82 - 64)) | (1L << (83 - 64)) | (1L << (86 - 64)) | (1L << (88 - 64)) | (1L << (89 - 64)) | (1L << (95 - 64)) | (1L << (97 - 64)) | (1L << (99 - 64)) | (1L << (100 - 64)) | (1L << (101 - 64)) | (1L << (107 - 64)) | (1L << (109 - 64)) | (1L << (110 - 64)) | (1L << (112 - 64)) | (1L << (IDENTIFIER - 64)) | (1L << (IMPORT - 64)) | (1L << (INCLUDE - 64)) | (1L << (COMMENT - 64)) | (1L << (LINE_COMMENT - 64)))) != 0) );
			State = 227; Match(ObjCParser.Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class External_declarationContext : ParserRuleContext {
		public Protocol_declarationContext protocol_declaration() {
			return GetRuleContext<Protocol_declarationContext>(0);
		}
		public DeclarationContext declaration() {
			return GetRuleContext<DeclarationContext>(0);
		}
		public Class_declaration_listContext class_declaration_list() {
			return GetRuleContext<Class_declaration_listContext>(0);
		}
		public Class_implementationContext class_implementation() {
			return GetRuleContext<Class_implementationContext>(0);
		}
		public ITerminalNode LINE_COMMENT() { return GetToken(ObjCParser.LINE_COMMENT, 0); }
		public ITerminalNode COMMENT() { return GetToken(ObjCParser.COMMENT, 0); }
		public Preprocessor_declarationContext preprocessor_declaration() {
			return GetRuleContext<Preprocessor_declarationContext>(0);
		}
		public Class_interfaceContext class_interface() {
			return GetRuleContext<Class_interfaceContext>(0);
		}
		public Function_definitionContext function_definition() {
			return GetRuleContext<Function_definitionContext>(0);
		}
		public Protocol_declaration_listContext protocol_declaration_list() {
			return GetRuleContext<Protocol_declaration_listContext>(0);
		}
		public Category_implementationContext category_implementation() {
			return GetRuleContext<Category_implementationContext>(0);
		}
		public Category_interfaceContext category_interface() {
			return GetRuleContext<Category_interfaceContext>(0);
		}
		public External_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_external_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterExternal_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitExternal_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExternal_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public External_declarationContext external_declaration() {
		External_declarationContext _localctx = new External_declarationContext(_ctx, State);
		EnterRule(_localctx, 2, RULE_external_declaration);
		try {
			State = 241;
			switch ( Interpreter.AdaptivePredict(_input,1,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 229; Match(COMMENT);
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 230; Match(LINE_COMMENT);
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 231; preprocessor_declaration();
				}
				break;

			case 4:
				EnterOuterAlt(_localctx, 4);
				{
				State = 232; function_definition();
				}
				break;

			case 5:
				EnterOuterAlt(_localctx, 5);
				{
				State = 233; declaration();
				}
				break;

			case 6:
				EnterOuterAlt(_localctx, 6);
				{
				State = 234; class_interface();
				}
				break;

			case 7:
				EnterOuterAlt(_localctx, 7);
				{
				State = 235; class_implementation();
				}
				break;

			case 8:
				EnterOuterAlt(_localctx, 8);
				{
				State = 236; category_interface();
				}
				break;

			case 9:
				EnterOuterAlt(_localctx, 9);
				{
				State = 237; category_implementation();
				}
				break;

			case 10:
				EnterOuterAlt(_localctx, 10);
				{
				State = 238; protocol_declaration();
				}
				break;

			case 11:
				EnterOuterAlt(_localctx, 11);
				{
				State = 239; protocol_declaration_list();
				}
				break;

			case 12:
				EnterOuterAlt(_localctx, 12);
				{
				State = 240; class_declaration_list();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Preprocessor_declarationContext : ParserRuleContext {
		public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public ITerminalNode IMPORT() { return GetToken(ObjCParser.IMPORT, 0); }
		public Macro_specificationContext macro_specification() {
			return GetRuleContext<Macro_specificationContext>(0);
		}
		public ITerminalNode INCLUDE() { return GetToken(ObjCParser.INCLUDE, 0); }
		public Preprocessor_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_preprocessor_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterPreprocessor_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitPreprocessor_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPreprocessor_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Preprocessor_declarationContext preprocessor_declaration() {
		Preprocessor_declarationContext _localctx = new Preprocessor_declarationContext(_ctx, State);
		EnterRule(_localctx, 4, RULE_preprocessor_declaration);
		try {
			State = 256;
			switch (_input.La(1)) {
			case IMPORT:
				EnterOuterAlt(_localctx, 1);
				{
				State = 243; Match(IMPORT);
				}
				break;
			case INCLUDE:
				EnterOuterAlt(_localctx, 2);
				{
				State = 244; Match(INCLUDE);
				}
				break;
			case 101:
				EnterOuterAlt(_localctx, 3);
				{
				State = 245; Match(101);
				State = 246; macro_specification();
				}
				break;
			case 21:
				EnterOuterAlt(_localctx, 4);
				{
				State = 247; Match(21);
				State = 248; expression();
				}
				break;
			case 112:
				EnterOuterAlt(_localctx, 5);
				{
				State = 249; Match(112);
				State = 250; expression();
				}
				break;
			case 99:
				EnterOuterAlt(_localctx, 6);
				{
				State = 251; Match(99);
				State = 252; expression();
				}
				break;
			case 64:
				EnterOuterAlt(_localctx, 7);
				{
				State = 253; Match(64);
				State = 254; expression();
				}
				break;
			case 97:
				EnterOuterAlt(_localctx, 8);
				{
				State = 255; Match(97);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Macro_specificationContext : ParserRuleContext {
		public Macro_specificationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_macro_specification; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterMacro_specification(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitMacro_specification(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMacro_specification(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Macro_specificationContext macro_specification() {
		Macro_specificationContext _localctx = new Macro_specificationContext(_ctx, State);
		EnterRule(_localctx, 6, RULE_macro_specification);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 258; Match(46);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Class_interfaceContext : ParserRuleContext {
		public Instance_variablesContext instance_variables() {
			return GetRuleContext<Instance_variablesContext>(0);
		}
		public Superclass_nameContext superclass_name() {
			return GetRuleContext<Superclass_nameContext>(0);
		}
		public Protocol_reference_listContext protocol_reference_list() {
			return GetRuleContext<Protocol_reference_listContext>(0);
		}
		public Interface_declaration_listContext interface_declaration_list() {
			return GetRuleContext<Interface_declaration_listContext>(0);
		}
		public Class_nameContext class_name() {
			return GetRuleContext<Class_nameContext>(0);
		}
		public Class_interfaceContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_class_interface; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterClass_interface(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitClass_interface(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitClass_interface(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Class_interfaceContext class_interface() {
		Class_interfaceContext _localctx = new Class_interfaceContext(_ctx, State);
		EnterRule(_localctx, 8, RULE_class_interface);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 260; Match(110);
			State = 261; class_name();
			State = 264;
			_la = _input.La(1);
			if (_la==94) {
				{
				State = 262; Match(94);
				State = 263; superclass_name();
				}
			}

			State = 267;
			_la = _input.La(1);
			if (_la==66) {
				{
				State = 266; protocol_reference_list();
				}
			}

			State = 270;
			_la = _input.La(1);
			if (_la==98) {
				{
				State = 269; instance_variables();
				}
			}

			State = 273;
			_la = _input.La(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 19) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 34) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 51) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (100 - 71)) | (1L << (107 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0)) {
				{
				State = 272; interface_declaration_list();
				}
			}

			State = 275; Match(65);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Category_interfaceContext : ParserRuleContext {
		public Category_nameContext category_name() {
			return GetRuleContext<Category_nameContext>(0);
		}
		public Protocol_reference_listContext protocol_reference_list() {
			return GetRuleContext<Protocol_reference_listContext>(0);
		}
		public Interface_declaration_listContext interface_declaration_list() {
			return GetRuleContext<Interface_declaration_listContext>(0);
		}
		public Class_nameContext class_name() {
			return GetRuleContext<Class_nameContext>(0);
		}
		public Category_interfaceContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_category_interface; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterCategory_interface(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitCategory_interface(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCategory_interface(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Category_interfaceContext category_interface() {
		Category_interfaceContext _localctx = new Category_interfaceContext(_ctx, State);
		EnterRule(_localctx, 10, RULE_category_interface);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 277; Match(110);
			State = 278; class_name();
			State = 279; Match(95);
			State = 281;
			_la = _input.La(1);
			if (_la==IDENTIFIER) {
				{
				State = 280; category_name();
				}
			}

			State = 283; Match(14);
			State = 285;
			_la = _input.La(1);
			if (_la==66) {
				{
				State = 284; protocol_reference_list();
				}
			}

			State = 288;
			_la = _input.La(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 19) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 34) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 51) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (100 - 71)) | (1L << (107 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0)) {
				{
				State = 287; interface_declaration_list();
				}
			}

			State = 290; Match(65);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Class_implementationContext : ParserRuleContext {
		public Implementation_definition_listContext implementation_definition_list() {
			return GetRuleContext<Implementation_definition_listContext>(0);
		}
		public Superclass_nameContext superclass_name() {
			return GetRuleContext<Superclass_nameContext>(0);
		}
		public Class_nameContext class_name() {
			return GetRuleContext<Class_nameContext>(0);
		}
		public Class_implementationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_class_implementation; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterClass_implementation(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitClass_implementation(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitClass_implementation(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Class_implementationContext class_implementation() {
		Class_implementationContext _localctx = new Class_implementationContext(_ctx, State);
		EnterRule(_localctx, 12, RULE_class_implementation);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 292; Match(17);
			{
			State = 293; class_name();
			State = 296;
			_la = _input.La(1);
			if (_la==94) {
				{
				State = 294; Match(94);
				State = 295; superclass_name();
				}
			}

			State = 299;
			_la = _input.La(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 3) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 34) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 51) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (77 - 71)) | (1L << (78 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (95 - 71)) | (1L << (100 - 71)) | (1L << (107 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0)) {
				{
				State = 298; implementation_definition_list();
				}
			}

			}
			State = 301; Match(65);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Category_implementationContext : ParserRuleContext {
		public Implementation_definition_listContext implementation_definition_list() {
			return GetRuleContext<Implementation_definition_listContext>(0);
		}
		public Category_nameContext category_name() {
			return GetRuleContext<Category_nameContext>(0);
		}
		public Class_nameContext class_name() {
			return GetRuleContext<Class_nameContext>(0);
		}
		public Category_implementationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_category_implementation; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterCategory_implementation(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitCategory_implementation(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCategory_implementation(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Category_implementationContext category_implementation() {
		Category_implementationContext _localctx = new Category_implementationContext(_ctx, State);
		EnterRule(_localctx, 14, RULE_category_implementation);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 303; Match(17);
			{
			State = 304; class_name();
			State = 305; Match(95);
			State = 306; category_name();
			State = 307; Match(14);
			State = 309;
			_la = _input.La(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 3) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 34) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 51) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (77 - 71)) | (1L << (78 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (95 - 71)) | (1L << (100 - 71)) | (1L << (107 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0)) {
				{
				State = 308; implementation_definition_list();
				}
			}

			}
			State = 311; Match(65);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Protocol_declarationContext : ParserRuleContext {
		public Protocol_reference_listContext protocol_reference_list() {
			return GetRuleContext<Protocol_reference_listContext>(0);
		}
		public Interface_declaration_listContext interface_declaration_list() {
			return GetRuleContext<Interface_declaration_listContext>(0);
		}
		public Protocol_nameContext protocol_name() {
			return GetRuleContext<Protocol_nameContext>(0);
		}
		public Protocol_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_protocol_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProtocol_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProtocol_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProtocol_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Protocol_declarationContext protocol_declaration() {
		Protocol_declarationContext _localctx = new Protocol_declarationContext(_ctx, State);
		EnterRule(_localctx, 16, RULE_protocol_declaration);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 313; Match(74);
			{
			State = 314; protocol_name();
			State = 316;
			_la = _input.La(1);
			if (_la==66) {
				{
				State = 315; protocol_reference_list();
				}
			}

			State = 319;
			_la = _input.La(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 19) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 34) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 51) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (100 - 71)) | (1L << (107 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0)) {
				{
				State = 318; interface_declaration_list();
				}
			}

			}
			State = 321; Match(65);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Protocol_declaration_listContext : ParserRuleContext {
		public Protocol_listContext protocol_list() {
			return GetRuleContext<Protocol_listContext>(0);
		}
		public Protocol_declaration_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_protocol_declaration_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProtocol_declaration_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProtocol_declaration_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProtocol_declaration_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Protocol_declaration_listContext protocol_declaration_list() {
		Protocol_declaration_listContext _localctx = new Protocol_declaration_listContext(_ctx, State);
		EnterRule(_localctx, 18, RULE_protocol_declaration_list);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 323; Match(74);
			State = 324; protocol_list();
			State = 325; Match(108);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Class_declaration_listContext : ParserRuleContext {
		public Class_listContext class_list() {
			return GetRuleContext<Class_listContext>(0);
		}
		public Class_declaration_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_class_declaration_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterClass_declaration_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitClass_declaration_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitClass_declaration_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Class_declaration_listContext class_declaration_list() {
		Class_declaration_listContext _localctx = new Class_declaration_listContext(_ctx, State);
		EnterRule(_localctx, 20, RULE_class_declaration_list);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 327; Match(89);
			State = 328; class_list();
			State = 329; Match(108);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Class_listContext : ParserRuleContext {
		public Class_nameContext[] class_name() {
			return GetRuleContexts<Class_nameContext>();
		}
		public Class_nameContext class_name(int i) {
			return GetRuleContext<Class_nameContext>(i);
		}
		public Class_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_class_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterClass_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitClass_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitClass_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Class_listContext class_list() {
		Class_listContext _localctx = new Class_listContext(_ctx, State);
		EnterRule(_localctx, 22, RULE_class_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 331; class_name();
			State = 336;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==31) {
				{
				{
				State = 332; Match(31);
				State = 333; class_name();
				}
				}
				State = 338;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Protocol_reference_listContext : ParserRuleContext {
		public Protocol_listContext protocol_list() {
			return GetRuleContext<Protocol_listContext>(0);
		}
		public Protocol_reference_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_protocol_reference_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProtocol_reference_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProtocol_reference_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProtocol_reference_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Protocol_reference_listContext protocol_reference_list() {
		Protocol_reference_listContext _localctx = new Protocol_reference_listContext(_ctx, State);
		EnterRule(_localctx, 24, RULE_protocol_reference_list);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 339; Match(66);
			State = 340; protocol_list();
			State = 341; Match(54);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Protocol_listContext : ParserRuleContext {
		public Protocol_nameContext protocol_name(int i) {
			return GetRuleContext<Protocol_nameContext>(i);
		}
		public Protocol_nameContext[] protocol_name() {
			return GetRuleContexts<Protocol_nameContext>();
		}
		public Protocol_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_protocol_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProtocol_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProtocol_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProtocol_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Protocol_listContext protocol_list() {
		Protocol_listContext _localctx = new Protocol_listContext(_ctx, State);
		EnterRule(_localctx, 26, RULE_protocol_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 343; protocol_name();
			State = 348;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==31) {
				{
				{
				State = 344; Match(31);
				State = 345; protocol_name();
				}
				}
				State = 350;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Property_declarationContext : ParserRuleContext {
		public Property_attributes_declarationContext property_attributes_declaration() {
			return GetRuleContext<Property_attributes_declarationContext>(0);
		}
		public Struct_declarationContext struct_declaration() {
			return GetRuleContext<Struct_declarationContext>(0);
		}
		public Property_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_property_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProperty_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProperty_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProperty_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Property_declarationContext property_declaration() {
		Property_declarationContext _localctx = new Property_declarationContext(_ctx, State);
		EnterRule(_localctx, 28, RULE_property_declaration);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 351; Match(19);
			State = 353;
			_la = _input.La(1);
			if (_la==95) {
				{
				State = 352; property_attributes_declaration();
				}
			}

			State = 355; struct_declaration();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Property_attributes_declarationContext : ParserRuleContext {
		public Property_attributes_listContext property_attributes_list() {
			return GetRuleContext<Property_attributes_listContext>(0);
		}
		public Property_attributes_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_property_attributes_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProperty_attributes_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProperty_attributes_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProperty_attributes_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Property_attributes_declarationContext property_attributes_declaration() {
		Property_attributes_declarationContext _localctx = new Property_attributes_declarationContext(_ctx, State);
		EnterRule(_localctx, 30, RULE_property_attributes_declaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 357; Match(95);
			State = 358; property_attributes_list();
			State = 359; Match(14);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Property_attributes_listContext : ParserRuleContext {
		public Property_attributeContext property_attribute(int i) {
			return GetRuleContext<Property_attributeContext>(i);
		}
		public Property_attributeContext[] property_attribute() {
			return GetRuleContexts<Property_attributeContext>();
		}
		public Property_attributes_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_property_attributes_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProperty_attributes_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProperty_attributes_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProperty_attributes_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Property_attributes_listContext property_attributes_list() {
		Property_attributes_listContext _localctx = new Property_attributes_listContext(_ctx, State);
		EnterRule(_localctx, 32, RULE_property_attributes_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 361; property_attribute();
			State = 366;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==31) {
				{
				{
				State = 362; Match(31);
				State = 363; property_attribute();
				}
				}
				State = 368;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Property_attributeContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER(int i) {
			return GetToken(ObjCParser.IDENTIFIER, i);
		}
		public ITerminalNode[] IDENTIFIER() { return GetTokens(ObjCParser.IDENTIFIER); }
		public Property_attributeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_property_attribute; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProperty_attribute(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProperty_attribute(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProperty_attribute(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Property_attributeContext property_attribute() {
		Property_attributeContext _localctx = new Property_attributeContext(_ctx, State);
		EnterRule(_localctx, 34, RULE_property_attribute);
		try {
			State = 377;
			switch ( Interpreter.AdaptivePredict(_input,19,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 369; Match(IDENTIFIER);
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 370; Match(IDENTIFIER);
				State = 371; Match(85);
				State = 372; Match(IDENTIFIER);
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 373; Match(IDENTIFIER);
				State = 374; Match(85);
				State = 375; Match(IDENTIFIER);
				State = 376; Match(94);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Class_nameContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Class_nameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_class_name; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterClass_name(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitClass_name(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitClass_name(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Class_nameContext class_name() {
		Class_nameContext _localctx = new Class_nameContext(_ctx, State);
		EnterRule(_localctx, 36, RULE_class_name);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 379; Match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Superclass_nameContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Superclass_nameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_superclass_name; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterSuperclass_name(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitSuperclass_name(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSuperclass_name(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Superclass_nameContext superclass_name() {
		Superclass_nameContext _localctx = new Superclass_nameContext(_ctx, State);
		EnterRule(_localctx, 38, RULE_superclass_name);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 381; Match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Category_nameContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Category_nameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_category_name; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterCategory_name(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitCategory_name(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCategory_name(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Category_nameContext category_name() {
		Category_nameContext _localctx = new Category_nameContext(_ctx, State);
		EnterRule(_localctx, 40, RULE_category_name);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 383; Match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Protocol_nameContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Protocol_nameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_protocol_name; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProtocol_name(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProtocol_name(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProtocol_name(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Protocol_nameContext protocol_name() {
		Protocol_nameContext _localctx = new Protocol_nameContext(_ctx, State);
		EnterRule(_localctx, 42, RULE_protocol_name);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 385; Match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Instance_variablesContext : ParserRuleContext {
		public Instance_variablesContext instance_variables() {
			return GetRuleContext<Instance_variablesContext>(0);
		}
		public Struct_declarationContext[] struct_declaration() {
			return GetRuleContexts<Struct_declarationContext>();
		}
		public Visibility_specificationContext visibility_specification() {
			return GetRuleContext<Visibility_specificationContext>(0);
		}
		public Struct_declarationContext struct_declaration(int i) {
			return GetRuleContext<Struct_declarationContext>(i);
		}
		public Instance_variablesContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_instance_variables; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterInstance_variables(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitInstance_variables(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInstance_variables(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Instance_variablesContext instance_variables() {
		Instance_variablesContext _localctx = new Instance_variablesContext(_ctx, State);
		EnterRule(_localctx, 44, RULE_instance_variables);
		int _la;
		try {
			State = 423;
			switch ( Interpreter.AdaptivePredict(_input,24,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 387; Match(98);
				State = 391;
				_errHandler.Sync(this);
				_la = _input.La(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0)) {
					{
					{
					State = 388; struct_declaration();
					}
					}
					State = 393;
					_errHandler.Sync(this);
					_la = _input.La(1);
				}
				State = 394; Match(7);
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 395; Match(98);
				State = 396; visibility_specification();
				State = 398;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 397; struct_declaration();
					}
					}
					State = 400;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0) );
				State = 402; Match(7);
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 404; Match(98);
				State = 406;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 405; struct_declaration();
					}
					}
					State = 408;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0) );
				State = 410; instance_variables();
				State = 411; Match(7);
				}
				break;

			case 4:
				EnterOuterAlt(_localctx, 4);
				{
				State = 413; Match(98);
				State = 414; visibility_specification();
				State = 416;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 415; struct_declaration();
					}
					}
					State = 418;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0) );
				State = 420; instance_variables();
				State = 421; Match(7);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Visibility_specificationContext : ParserRuleContext {
		public Visibility_specificationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_visibility_specification; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterVisibility_specification(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitVisibility_specification(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitVisibility_specification(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Visibility_specificationContext visibility_specification() {
		Visibility_specificationContext _localctx = new Visibility_specificationContext(_ctx, State);
		EnterRule(_localctx, 46, RULE_visibility_specification);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 425;
			_la = _input.La(1);
			if ( !(_la==27 || ((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (76 - 76)) | (1L << (93 - 76)) | (1L << (104 - 76)))) != 0)) ) {
			_errHandler.RecoverInline(this);
			}
			Consume();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Interface_declaration_listContext : ParserRuleContext {
		public Class_method_declarationContext class_method_declaration(int i) {
			return GetRuleContext<Class_method_declarationContext>(i);
		}
		public Property_declarationContext property_declaration(int i) {
			return GetRuleContext<Property_declarationContext>(i);
		}
		public DeclarationContext[] declaration() {
			return GetRuleContexts<DeclarationContext>();
		}
		public Property_declarationContext[] property_declaration() {
			return GetRuleContexts<Property_declarationContext>();
		}
		public Instance_method_declarationContext instance_method_declaration(int i) {
			return GetRuleContext<Instance_method_declarationContext>(i);
		}
		public DeclarationContext declaration(int i) {
			return GetRuleContext<DeclarationContext>(i);
		}
		public Instance_method_declarationContext[] instance_method_declaration() {
			return GetRuleContexts<Instance_method_declarationContext>();
		}
		public Class_method_declarationContext[] class_method_declaration() {
			return GetRuleContexts<Class_method_declarationContext>();
		}
		public Interface_declaration_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_interface_declaration_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterInterface_declaration_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitInterface_declaration_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInterface_declaration_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Interface_declaration_listContext interface_declaration_list() {
		Interface_declaration_listContext _localctx = new Interface_declaration_listContext(_ctx, State);
		EnterRule(_localctx, 48, RULE_interface_declaration_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 431;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				State = 431;
				switch (_input.La(1)) {
				case 2:
				case 5:
				case 6:
				case 8:
				case 9:
				case 11:
				case 13:
				case 16:
				case 20:
				case 25:
				case 28:
				case 30:
				case 36:
				case 38:
				case 39:
				case 47:
				case 49:
				case 60:
				case 71:
				case 82:
				case 83:
				case 86:
				case 88:
				case 100:
				case 107:
				case 109:
				case IDENTIFIER:
					{
					State = 427; declaration();
					}
					break;
				case 51:
					{
					State = 428; class_method_declaration();
					}
					break;
				case 34:
					{
					State = 429; instance_method_declaration();
					}
					break;
				case 19:
					{
					State = 430; property_declaration();
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				State = 433;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 19) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 34) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 51) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (100 - 71)) | (1L << (107 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Class_method_declarationContext : ParserRuleContext {
		public Method_declarationContext method_declaration() {
			return GetRuleContext<Method_declarationContext>(0);
		}
		public Class_method_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_class_method_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterClass_method_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitClass_method_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitClass_method_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Class_method_declarationContext class_method_declaration() {
		Class_method_declarationContext _localctx = new Class_method_declarationContext(_ctx, State);
		EnterRule(_localctx, 50, RULE_class_method_declaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 435; Match(51);
			State = 436; method_declaration();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Instance_method_declarationContext : ParserRuleContext {
		public Method_declarationContext method_declaration() {
			return GetRuleContext<Method_declarationContext>(0);
		}
		public Instance_method_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_instance_method_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterInstance_method_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitInstance_method_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInstance_method_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Instance_method_declarationContext instance_method_declaration() {
		Instance_method_declarationContext _localctx = new Instance_method_declarationContext(_ctx, State);
		EnterRule(_localctx, 52, RULE_instance_method_declaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 438; Match(34);
			State = 439; method_declaration();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Method_declarationContext : ParserRuleContext {
		public Method_selectorContext method_selector() {
			return GetRuleContext<Method_selectorContext>(0);
		}
		public Method_typeContext method_type() {
			return GetRuleContext<Method_typeContext>(0);
		}
		public Method_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_method_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterMethod_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitMethod_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMethod_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Method_declarationContext method_declaration() {
		Method_declarationContext _localctx = new Method_declarationContext(_ctx, State);
		EnterRule(_localctx, 54, RULE_method_declaration);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 442;
			_la = _input.La(1);
			if (_la==95) {
				{
				State = 441; method_type();
				}
			}

			State = 444; method_selector();
			State = 445; Match(108);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Implementation_definition_listContext : ParserRuleContext {
		public Instance_method_definitionContext[] instance_method_definition() {
			return GetRuleContexts<Instance_method_definitionContext>();
		}
		public Class_method_definitionContext class_method_definition(int i) {
			return GetRuleContext<Class_method_definitionContext>(i);
		}
		public Instance_method_definitionContext instance_method_definition(int i) {
			return GetRuleContext<Instance_method_definitionContext>(i);
		}
		public DeclarationContext[] declaration() {
			return GetRuleContexts<DeclarationContext>();
		}
		public Property_implementationContext[] property_implementation() {
			return GetRuleContexts<Property_implementationContext>();
		}
		public Function_definitionContext function_definition(int i) {
			return GetRuleContext<Function_definitionContext>(i);
		}
		public DeclarationContext declaration(int i) {
			return GetRuleContext<DeclarationContext>(i);
		}
		public Property_implementationContext property_implementation(int i) {
			return GetRuleContext<Property_implementationContext>(i);
		}
		public Function_definitionContext[] function_definition() {
			return GetRuleContexts<Function_definitionContext>();
		}
		public Class_method_definitionContext[] class_method_definition() {
			return GetRuleContexts<Class_method_definitionContext>();
		}
		public Implementation_definition_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_implementation_definition_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterImplementation_definition_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitImplementation_definition_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitImplementation_definition_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Implementation_definition_listContext implementation_definition_list() {
		Implementation_definition_listContext _localctx = new Implementation_definition_listContext(_ctx, State);
		EnterRule(_localctx, 56, RULE_implementation_definition_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 452;
			_errHandler.Sync(this);
			_la = _input.La(1);
			do {
				{
				State = 452;
				switch ( Interpreter.AdaptivePredict(_input,28,_ctx) ) {
				case 1:
					{
					State = 447; function_definition();
					}
					break;

				case 2:
					{
					State = 448; declaration();
					}
					break;

				case 3:
					{
					State = 449; class_method_definition();
					}
					break;

				case 4:
					{
					State = 450; instance_method_definition();
					}
					break;

				case 5:
					{
					State = 451; property_implementation();
					}
					break;
				}
				}
				State = 454;
				_errHandler.Sync(this);
				_la = _input.La(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 3) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 34) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 51) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (77 - 71)) | (1L << (78 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (95 - 71)) | (1L << (100 - 71)) | (1L << (107 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Class_method_definitionContext : ParserRuleContext {
		public Method_definitionContext method_definition() {
			return GetRuleContext<Method_definitionContext>(0);
		}
		public Class_method_definitionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_class_method_definition; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterClass_method_definition(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitClass_method_definition(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitClass_method_definition(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Class_method_definitionContext class_method_definition() {
		Class_method_definitionContext _localctx = new Class_method_definitionContext(_ctx, State);
		EnterRule(_localctx, 58, RULE_class_method_definition);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 456; Match(51);
			State = 457; method_definition();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Instance_method_definitionContext : ParserRuleContext {
		public Method_definitionContext method_definition() {
			return GetRuleContext<Method_definitionContext>(0);
		}
		public Instance_method_definitionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_instance_method_definition; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterInstance_method_definition(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitInstance_method_definition(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInstance_method_definition(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Instance_method_definitionContext instance_method_definition() {
		Instance_method_definitionContext _localctx = new Instance_method_definitionContext(_ctx, State);
		EnterRule(_localctx, 60, RULE_instance_method_definition);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			{
			State = 459; Match(34);
			State = 460; method_definition();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Method_definitionContext : ParserRuleContext {
		public Method_selectorContext method_selector() {
			return GetRuleContext<Method_selectorContext>(0);
		}
		public Init_declarator_listContext init_declarator_list() {
			return GetRuleContext<Init_declarator_listContext>(0);
		}
		public Compound_statementContext compound_statement() {
			return GetRuleContext<Compound_statementContext>(0);
		}
		public Method_typeContext method_type() {
			return GetRuleContext<Method_typeContext>(0);
		}
		public Method_definitionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_method_definition; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterMethod_definition(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitMethod_definition(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMethod_definition(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Method_definitionContext method_definition() {
		Method_definitionContext _localctx = new Method_definitionContext(_ctx, State);
		EnterRule(_localctx, 62, RULE_method_definition);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 463;
			_la = _input.La(1);
			if (_la==95) {
				{
				State = 462; method_type();
				}
			}

			State = 465; method_selector();
			State = 467;
			_la = _input.La(1);
			if (_la==3 || _la==95 || _la==IDENTIFIER) {
				{
				State = 466; init_declarator_list();
				}
			}

			State = 469; compound_statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Method_selectorContext : ParserRuleContext {
		public SelectorContext selector() {
			return GetRuleContext<SelectorContext>(0);
		}
		public Keyword_declaratorContext[] keyword_declarator() {
			return GetRuleContexts<Keyword_declaratorContext>();
		}
		public Keyword_declaratorContext keyword_declarator(int i) {
			return GetRuleContext<Keyword_declaratorContext>(i);
		}
		public Parameter_listContext parameter_list() {
			return GetRuleContext<Parameter_listContext>(0);
		}
		public Method_selectorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_method_selector; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterMethod_selector(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitMethod_selector(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMethod_selector(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Method_selectorContext method_selector() {
		Method_selectorContext _localctx = new Method_selectorContext(_ctx, State);
		EnterRule(_localctx, 64, RULE_method_selector);
		try {
			int _alt;
			State = 480;
			switch ( Interpreter.AdaptivePredict(_input,34,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 471; selector();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				{
				State = 473;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,32,_ctx);
				do {
					switch (_alt) {
					case 1:
						{
						{
						State = 472; keyword_declarator();
						}
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					State = 475;
					_errHandler.Sync(this);
					_alt = Interpreter.AdaptivePredict(_input,32,_ctx);
				} while ( _alt!=2 && _alt!=-1 );
				State = 478;
				switch ( Interpreter.AdaptivePredict(_input,33,_ctx) ) {
				case 1:
					{
					State = 477; parameter_list();
					}
					break;
				}
				}
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Keyword_declaratorContext : ParserRuleContext {
		public SelectorContext selector() {
			return GetRuleContext<SelectorContext>(0);
		}
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Method_typeContext[] method_type() {
			return GetRuleContexts<Method_typeContext>();
		}
		public Method_typeContext method_type(int i) {
			return GetRuleContext<Method_typeContext>(i);
		}
		public Keyword_declaratorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_keyword_declarator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterKeyword_declarator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitKeyword_declarator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitKeyword_declarator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Keyword_declaratorContext keyword_declarator() {
		Keyword_declaratorContext _localctx = new Keyword_declaratorContext(_ctx, State);
		EnterRule(_localctx, 66, RULE_keyword_declarator);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 483;
			_la = _input.La(1);
			if (_la==IDENTIFIER) {
				{
				State = 482; selector();
				}
			}

			State = 485; Match(94);
			State = 489;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==95) {
				{
				{
				State = 486; method_type();
				}
				}
				State = 491;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			State = 492; Match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class SelectorContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public SelectorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_selector; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterSelector(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitSelector(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSelector(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public SelectorContext selector() {
		SelectorContext _localctx = new SelectorContext(_ctx, State);
		EnterRule(_localctx, 68, RULE_selector);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 494; Match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Method_typeContext : ParserRuleContext {
		public Type_nameContext type_name() {
			return GetRuleContext<Type_nameContext>(0);
		}
		public Method_typeContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_method_type; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterMethod_type(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitMethod_type(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMethod_type(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Method_typeContext method_type() {
		Method_typeContext _localctx = new Method_typeContext(_ctx, State);
		EnterRule(_localctx, 70, RULE_method_type);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 496; Match(95);
			State = 497; type_name();
			State = 498; Match(14);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Property_implementationContext : ParserRuleContext {
		public Property_synthesize_listContext property_synthesize_list() {
			return GetRuleContext<Property_synthesize_listContext>(0);
		}
		public Property_implementationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_property_implementation; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProperty_implementation(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProperty_implementation(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProperty_implementation(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Property_implementationContext property_implementation() {
		Property_implementationContext _localctx = new Property_implementationContext(_ctx, State);
		EnterRule(_localctx, 72, RULE_property_implementation);
		try {
			State = 508;
			switch (_input.La(1)) {
			case 77:
				EnterOuterAlt(_localctx, 1);
				{
				State = 500; Match(77);
				State = 501; property_synthesize_list();
				State = 502; Match(108);
				}
				break;
			case 78:
				EnterOuterAlt(_localctx, 2);
				{
				State = 504; Match(78);
				State = 505; property_synthesize_list();
				State = 506; Match(108);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Property_synthesize_listContext : ParserRuleContext {
		public Property_synthesize_itemContext[] property_synthesize_item() {
			return GetRuleContexts<Property_synthesize_itemContext>();
		}
		public Property_synthesize_itemContext property_synthesize_item(int i) {
			return GetRuleContext<Property_synthesize_itemContext>(i);
		}
		public Property_synthesize_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_property_synthesize_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProperty_synthesize_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProperty_synthesize_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProperty_synthesize_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Property_synthesize_listContext property_synthesize_list() {
		Property_synthesize_listContext _localctx = new Property_synthesize_listContext(_ctx, State);
		EnterRule(_localctx, 74, RULE_property_synthesize_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 510; property_synthesize_item();
			State = 515;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==31) {
				{
				{
				State = 511; Match(31);
				State = 512; property_synthesize_item();
				}
				}
				State = 517;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Property_synthesize_itemContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER(int i) {
			return GetToken(ObjCParser.IDENTIFIER, i);
		}
		public ITerminalNode[] IDENTIFIER() { return GetTokens(ObjCParser.IDENTIFIER); }
		public Property_synthesize_itemContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_property_synthesize_item; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProperty_synthesize_item(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProperty_synthesize_item(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProperty_synthesize_item(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Property_synthesize_itemContext property_synthesize_item() {
		Property_synthesize_itemContext _localctx = new Property_synthesize_itemContext(_ctx, State);
		EnterRule(_localctx, 76, RULE_property_synthesize_item);
		try {
			State = 522;
			switch ( Interpreter.AdaptivePredict(_input,39,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 518; Match(IDENTIFIER);
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 519; Match(IDENTIFIER);
				State = 520; Match(85);
				State = 521; Match(IDENTIFIER);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Type_specifierContext : ParserRuleContext {
		public Struct_or_union_specifierContext struct_or_union_specifier() {
			return GetRuleContext<Struct_or_union_specifierContext>(0);
		}
		public Protocol_reference_listContext protocol_reference_list() {
			return GetRuleContext<Protocol_reference_listContext>(0);
		}
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Class_nameContext class_name() {
			return GetRuleContext<Class_nameContext>(0);
		}
		public Enum_specifierContext enum_specifier() {
			return GetRuleContext<Enum_specifierContext>(0);
		}
		public Type_specifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_type_specifier; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterType_specifier(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitType_specifier(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitType_specifier(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Type_specifierContext type_specifier() {
		Type_specifierContext _localctx = new Type_specifierContext(_ctx, State);
		EnterRule(_localctx, 78, RULE_type_specifier);
		int _la;
		try {
			State = 544;
			switch ( Interpreter.AdaptivePredict(_input,42,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 524; Match(38);
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 525; Match(9);
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 526; Match(28);
				}
				break;

			case 4:
				EnterOuterAlt(_localctx, 4);
				{
				State = 527; Match(36);
				}
				break;

			case 5:
				EnterOuterAlt(_localctx, 5);
				{
				State = 528; Match(25);
				}
				break;

			case 6:
				EnterOuterAlt(_localctx, 6);
				{
				State = 529; Match(8);
				}
				break;

			case 7:
				EnterOuterAlt(_localctx, 7);
				{
				State = 530; Match(5);
				}
				break;

			case 8:
				EnterOuterAlt(_localctx, 8);
				{
				State = 531; Match(83);
				}
				break;

			case 9:
				EnterOuterAlt(_localctx, 9);
				{
				State = 532; Match(16);
				}
				break;

			case 10:
				EnterOuterAlt(_localctx, 10);
				{
				{
				State = 533; Match(71);
				State = 535;
				_la = _input.La(1);
				if (_la==66) {
					{
					State = 534; protocol_reference_list();
					}
				}

				}
				}
				break;

			case 11:
				EnterOuterAlt(_localctx, 11);
				{
				{
				State = 537; class_name();
				State = 539;
				_la = _input.La(1);
				if (_la==66) {
					{
					State = 538; protocol_reference_list();
					}
				}

				}
				}
				break;

			case 12:
				EnterOuterAlt(_localctx, 12);
				{
				State = 541; struct_or_union_specifier();
				}
				break;

			case 13:
				EnterOuterAlt(_localctx, 13);
				{
				State = 542; enum_specifier();
				}
				break;

			case 14:
				EnterOuterAlt(_localctx, 14);
				{
				State = 543; Match(IDENTIFIER);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Type_qualifierContext : ParserRuleContext {
		public Protocol_qualifierContext protocol_qualifier() {
			return GetRuleContext<Protocol_qualifierContext>(0);
		}
		public Type_qualifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_type_qualifier; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterType_qualifier(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitType_qualifier(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitType_qualifier(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Type_qualifierContext type_qualifier() {
		Type_qualifierContext _localctx = new Type_qualifierContext(_ctx, State);
		EnterRule(_localctx, 80, RULE_type_qualifier);
		try {
			State = 549;
			switch (_input.La(1)) {
			case 86:
				EnterOuterAlt(_localctx, 1);
				{
				State = 546; Match(86);
				}
				break;
			case 109:
				EnterOuterAlt(_localctx, 2);
				{
				State = 547; Match(109);
				}
				break;
			case 6:
			case 13:
			case 20:
			case 30:
			case 39:
			case 60:
				EnterOuterAlt(_localctx, 3);
				{
				State = 548; protocol_qualifier();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Protocol_qualifierContext : ParserRuleContext {
		public Protocol_qualifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_protocol_qualifier; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProtocol_qualifier(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProtocol_qualifier(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProtocol_qualifier(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Protocol_qualifierContext protocol_qualifier() {
		Protocol_qualifierContext _localctx = new Protocol_qualifierContext(_ctx, State);
		EnterRule(_localctx, 82, RULE_protocol_qualifier);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 551;
			_la = _input.La(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 6) | (1L << 13) | (1L << 20) | (1L << 30) | (1L << 39) | (1L << 60))) != 0)) ) {
			_errHandler.RecoverInline(this);
			}
			Consume();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Primary_expressionContext : ParserRuleContext {
		public Encode_expressionContext encode_expression() {
			return GetRuleContext<Encode_expressionContext>(0);
		}
		public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public ConstantContext constant() {
			return GetRuleContext<ConstantContext>(0);
		}
		public ITerminalNode STRING_LITERAL() { return GetToken(ObjCParser.STRING_LITERAL, 0); }
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Selector_expressionContext selector_expression() {
			return GetRuleContext<Selector_expressionContext>(0);
		}
		public Message_expressionContext message_expression() {
			return GetRuleContext<Message_expressionContext>(0);
		}
		public Protocol_expressionContext protocol_expression() {
			return GetRuleContext<Protocol_expressionContext>(0);
		}
		public Primary_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_primary_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterPrimary_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitPrimary_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPrimary_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Primary_expressionContext primary_expression() {
		Primary_expressionContext _localctx = new Primary_expressionContext(_ctx, State);
		EnterRule(_localctx, 84, RULE_primary_expression);
		try {
			State = 565;
			switch (_input.La(1)) {
			case IDENTIFIER:
				EnterOuterAlt(_localctx, 1);
				{
				State = 553; Match(IDENTIFIER);
				}
				break;
			case CHARACTER_LITERAL:
			case HEX_LITERAL:
			case DECIMAL_LITERAL:
			case OCTAL_LITERAL:
			case FLOATING_POINT_LITERAL:
				EnterOuterAlt(_localctx, 2);
				{
				State = 554; constant();
				}
				break;
			case STRING_LITERAL:
				EnterOuterAlt(_localctx, 3);
				{
				State = 555; Match(STRING_LITERAL);
				}
				break;
			case 95:
				EnterOuterAlt(_localctx, 4);
				{
				{
				State = 556; Match(95);
				State = 557; expression();
				State = 558; Match(14);
				}
				}
				break;
			case 1:
				EnterOuterAlt(_localctx, 5);
				{
				State = 560; Match(1);
				}
				break;
			case 63:
				EnterOuterAlt(_localctx, 6);
				{
				State = 561; message_expression();
				}
				break;
			case 73:
				EnterOuterAlt(_localctx, 7);
				{
				State = 562; selector_expression();
				}
				break;
			case 74:
				EnterOuterAlt(_localctx, 8);
				{
				State = 563; protocol_expression();
				}
				break;
			case 22:
				EnterOuterAlt(_localctx, 9);
				{
				State = 564; encode_expression();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Message_expressionContext : ParserRuleContext {
		public ReceiverContext receiver() {
			return GetRuleContext<ReceiverContext>(0);
		}
		public Message_selectorContext message_selector() {
			return GetRuleContext<Message_selectorContext>(0);
		}
		public Message_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_message_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterMessage_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitMessage_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMessage_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Message_expressionContext message_expression() {
		Message_expressionContext _localctx = new Message_expressionContext(_ctx, State);
		EnterRule(_localctx, 86, RULE_message_expression);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 567; Match(63);
			State = 568; receiver();
			State = 569; message_selector();
			State = 570; Match(91);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ReceiverContext : ParserRuleContext {
		public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public Class_nameContext class_name() {
			return GetRuleContext<Class_nameContext>(0);
		}
		public ReceiverContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_receiver; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterReceiver(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitReceiver(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitReceiver(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ReceiverContext receiver() {
		ReceiverContext _localctx = new ReceiverContext(_ctx, State);
		EnterRule(_localctx, 88, RULE_receiver);
		try {
			State = 575;
			switch ( Interpreter.AdaptivePredict(_input,45,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 572; expression();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 573; class_name();
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 574; Match(79);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Message_selectorContext : ParserRuleContext {
		public SelectorContext selector() {
			return GetRuleContext<SelectorContext>(0);
		}
		public Keyword_argumentContext[] keyword_argument() {
			return GetRuleContexts<Keyword_argumentContext>();
		}
		public Keyword_argumentContext keyword_argument(int i) {
			return GetRuleContext<Keyword_argumentContext>(i);
		}
		public Message_selectorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_message_selector; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterMessage_selector(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitMessage_selector(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMessage_selector(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Message_selectorContext message_selector() {
		Message_selectorContext _localctx = new Message_selectorContext(_ctx, State);
		EnterRule(_localctx, 90, RULE_message_selector);
		int _la;
		try {
			State = 583;
			switch ( Interpreter.AdaptivePredict(_input,47,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 577; selector();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 579;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 578; keyword_argument();
					}
					}
					State = 581;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( _la==94 || _la==IDENTIFIER );
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Keyword_argumentContext : ParserRuleContext {
		public SelectorContext selector() {
			return GetRuleContext<SelectorContext>(0);
		}
		public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public Keyword_argumentContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_keyword_argument; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterKeyword_argument(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitKeyword_argument(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitKeyword_argument(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Keyword_argumentContext keyword_argument() {
		Keyword_argumentContext _localctx = new Keyword_argumentContext(_ctx, State);
		EnterRule(_localctx, 92, RULE_keyword_argument);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 586;
			_la = _input.La(1);
			if (_la==IDENTIFIER) {
				{
				State = 585; selector();
				}
			}

			State = 588; Match(94);
			State = 589; expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Selector_expressionContext : ParserRuleContext {
		public Selector_nameContext selector_name() {
			return GetRuleContext<Selector_nameContext>(0);
		}
		public Selector_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_selector_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterSelector_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitSelector_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSelector_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Selector_expressionContext selector_expression() {
		Selector_expressionContext _localctx = new Selector_expressionContext(_ctx, State);
		EnterRule(_localctx, 94, RULE_selector_expression);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 591; Match(73);
			State = 592; Match(95);
			State = 593; selector_name();
			State = 594; Match(14);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Selector_nameContext : ParserRuleContext {
		public SelectorContext[] selector() {
			return GetRuleContexts<SelectorContext>();
		}
		public SelectorContext selector(int i) {
			return GetRuleContext<SelectorContext>(i);
		}
		public Selector_nameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_selector_name; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterSelector_name(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitSelector_name(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSelector_name(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Selector_nameContext selector_name() {
		Selector_nameContext _localctx = new Selector_nameContext(_ctx, State);
		EnterRule(_localctx, 96, RULE_selector_name);
		int _la;
		try {
			State = 605;
			switch ( Interpreter.AdaptivePredict(_input,51,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 596; selector();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 601;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 598;
					_la = _input.La(1);
					if (_la==IDENTIFIER) {
						{
						State = 597; selector();
						}
					}

					State = 600; Match(94);
					}
					}
					State = 603;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( _la==94 || _la==IDENTIFIER );
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Protocol_expressionContext : ParserRuleContext {
		public Protocol_nameContext protocol_name() {
			return GetRuleContext<Protocol_nameContext>(0);
		}
		public Protocol_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_protocol_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterProtocol_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitProtocol_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitProtocol_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Protocol_expressionContext protocol_expression() {
		Protocol_expressionContext _localctx = new Protocol_expressionContext(_ctx, State);
		EnterRule(_localctx, 98, RULE_protocol_expression);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 607; Match(74);
			State = 608; Match(95);
			State = 609; protocol_name();
			State = 610; Match(14);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Encode_expressionContext : ParserRuleContext {
		public Type_nameContext type_name() {
			return GetRuleContext<Type_nameContext>(0);
		}
		public Encode_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_encode_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterEncode_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitEncode_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEncode_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Encode_expressionContext encode_expression() {
		Encode_expressionContext _localctx = new Encode_expressionContext(_ctx, State);
		EnterRule(_localctx, 100, RULE_encode_expression);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 612; Match(22);
			State = 613; Match(95);
			State = 614; type_name();
			State = 615; Match(14);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Exception_declaratorContext : ParserRuleContext {
		public DeclaratorContext declarator() {
			return GetRuleContext<DeclaratorContext>(0);
		}
		public Exception_declaratorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_exception_declarator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterException_declarator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitException_declarator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitException_declarator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Exception_declaratorContext exception_declarator() {
		Exception_declaratorContext _localctx = new Exception_declaratorContext(_ctx, State);
		EnterRule(_localctx, 102, RULE_exception_declarator);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 617; declarator();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Try_statementContext : ParserRuleContext {
		public Try_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_try_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterTry_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitTry_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitTry_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Try_statementContext try_statement() {
		Try_statementContext _localctx = new Try_statementContext(_ctx, State);
		EnterRule(_localctx, 104, RULE_try_statement);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 619; Match(15);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Catch_statementContext : ParserRuleContext {
		public StatementContext statement() {
			return GetRuleContext<StatementContext>(0);
		}
		public Exception_declaratorContext exception_declarator() {
			return GetRuleContext<Exception_declaratorContext>(0);
		}
		public Catch_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_catch_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterCatch_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitCatch_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCatch_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Catch_statementContext catch_statement() {
		Catch_statementContext _localctx = new Catch_statementContext(_ctx, State);
		EnterRule(_localctx, 106, RULE_catch_statement);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 621; Match(84);
			State = 622; Match(95);
			State = 623; exception_declarator();
			State = 624; Match(14);
			State = 625; statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Finally_statementContext : ParserRuleContext {
		public StatementContext statement() {
			return GetRuleContext<StatementContext>(0);
		}
		public Finally_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_finally_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterFinally_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitFinally_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFinally_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Finally_statementContext finally_statement() {
		Finally_statementContext _localctx = new Finally_statementContext(_ctx, State);
		EnterRule(_localctx, 108, RULE_finally_statement);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 627; Match(33);
			State = 628; statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Throw_statementContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Throw_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_throw_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterThrow_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitThrow_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitThrow_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Throw_statementContext throw_statement() {
		Throw_statementContext _localctx = new Throw_statementContext(_ctx, State);
		EnterRule(_localctx, 110, RULE_throw_statement);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 630; Match(61);
			State = 631; Match(95);
			State = 632; Match(IDENTIFIER);
			State = 633; Match(14);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Try_blockContext : ParserRuleContext {
		public Try_statementContext try_statement() {
			return GetRuleContext<Try_statementContext>(0);
		}
		public Catch_statementContext catch_statement() {
			return GetRuleContext<Catch_statementContext>(0);
		}
		public Finally_statementContext finally_statement() {
			return GetRuleContext<Finally_statementContext>(0);
		}
		public Try_blockContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_try_block; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterTry_block(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitTry_block(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitTry_block(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Try_blockContext try_block() {
		Try_blockContext _localctx = new Try_blockContext(_ctx, State);
		EnterRule(_localctx, 112, RULE_try_block);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 635; try_statement();
			State = 636; catch_statement();
			State = 638;
			_la = _input.La(1);
			if (_la==33) {
				{
				State = 637; finally_statement();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Synchronized_statementContext : ParserRuleContext {
		public StatementContext statement() {
			return GetRuleContext<StatementContext>(0);
		}
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Synchronized_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_synchronized_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterSynchronized_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitSynchronized_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSynchronized_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Synchronized_statementContext synchronized_statement() {
		Synchronized_statementContext _localctx = new Synchronized_statementContext(_ctx, State);
		EnterRule(_localctx, 114, RULE_synchronized_statement);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 640; Match(4);
			State = 641; Match(95);
			State = 642; Match(IDENTIFIER);
			State = 643; Match(14);
			State = 644; statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Function_definitionContext : ParserRuleContext {
		public Declaration_specifiersContext declaration_specifiers() {
			return GetRuleContext<Declaration_specifiersContext>(0);
		}
		public DeclaratorContext declarator() {
			return GetRuleContext<DeclaratorContext>(0);
		}
		public Compound_statementContext compound_statement() {
			return GetRuleContext<Compound_statementContext>(0);
		}
		public Function_definitionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_function_definition; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterFunction_definition(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitFunction_definition(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitFunction_definition(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Function_definitionContext function_definition() {
		Function_definitionContext _localctx = new Function_definitionContext(_ctx, State);
		EnterRule(_localctx, 116, RULE_function_definition);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 647;
			switch ( Interpreter.AdaptivePredict(_input,53,_ctx) ) {
			case 1:
				{
				State = 646; declaration_specifiers();
				}
				break;
			}
			State = 649; declarator();
			State = 650; compound_statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DeclarationContext : ParserRuleContext {
		public Declaration_specifiersContext declaration_specifiers() {
			return GetRuleContext<Declaration_specifiersContext>(0);
		}
		public Init_declarator_listContext init_declarator_list() {
			return GetRuleContext<Init_declarator_listContext>(0);
		}
		public DeclarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterDeclaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitDeclaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDeclaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DeclarationContext declaration() {
		DeclarationContext _localctx = new DeclarationContext(_ctx, State);
		EnterRule(_localctx, 118, RULE_declaration);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 652; declaration_specifiers();
			State = 654;
			_la = _input.La(1);
			if (_la==3 || _la==95 || _la==IDENTIFIER) {
				{
				State = 653; init_declarator_list();
				}
			}

			State = 656; Match(108);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Declaration_specifiersContext : ParserRuleContext {
		public Type_qualifierContext type_qualifier(int i) {
			return GetRuleContext<Type_qualifierContext>(i);
		}
		public Storage_class_specifierContext storage_class_specifier(int i) {
			return GetRuleContext<Storage_class_specifierContext>(i);
		}
		public Type_specifierContext type_specifier(int i) {
			return GetRuleContext<Type_specifierContext>(i);
		}
		public Type_specifierContext[] type_specifier() {
			return GetRuleContexts<Type_specifierContext>();
		}
		public Storage_class_specifierContext[] storage_class_specifier() {
			return GetRuleContexts<Storage_class_specifierContext>();
		}
		public Type_qualifierContext[] type_qualifier() {
			return GetRuleContexts<Type_qualifierContext>();
		}
		public Declaration_specifiersContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_declaration_specifiers; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterDeclaration_specifiers(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitDeclaration_specifiers(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDeclaration_specifiers(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Declaration_specifiersContext declaration_specifiers() {
		Declaration_specifiersContext _localctx = new Declaration_specifiersContext(_ctx, State);
		EnterRule(_localctx, 120, RULE_declaration_specifiers);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 661;
			_errHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(_input,56,_ctx);
			do {
				switch (_alt) {
				case 1:
					{
					State = 661;
					switch (_input.La(1)) {
					case 2:
					case 11:
					case 49:
					case 100:
					case 107:
						{
						State = 658; storage_class_specifier();
						}
						break;
					case 5:
					case 8:
					case 9:
					case 16:
					case 25:
					case 28:
					case 36:
					case 38:
					case 47:
					case 71:
					case 82:
					case 83:
					case 88:
					case IDENTIFIER:
						{
						State = 659; type_specifier();
						}
						break;
					case 6:
					case 13:
					case 20:
					case 30:
					case 39:
					case 60:
					case 86:
					case 109:
						{
						State = 660; type_qualifier();
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				State = 663;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,56,_ctx);
			} while ( _alt!=2 && _alt!=-1 );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Storage_class_specifierContext : ParserRuleContext {
		public Storage_class_specifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_storage_class_specifier; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterStorage_class_specifier(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitStorage_class_specifier(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStorage_class_specifier(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Storage_class_specifierContext storage_class_specifier() {
		Storage_class_specifierContext _localctx = new Storage_class_specifierContext(_ctx, State);
		EnterRule(_localctx, 122, RULE_storage_class_specifier);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 665;
			_la = _input.La(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 11) | (1L << 49))) != 0) || _la==100 || _la==107) ) {
			_errHandler.RecoverInline(this);
			}
			Consume();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Init_declarator_listContext : ParserRuleContext {
		public Init_declaratorContext[] init_declarator() {
			return GetRuleContexts<Init_declaratorContext>();
		}
		public Init_declaratorContext init_declarator(int i) {
			return GetRuleContext<Init_declaratorContext>(i);
		}
		public Init_declarator_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_init_declarator_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterInit_declarator_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitInit_declarator_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInit_declarator_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Init_declarator_listContext init_declarator_list() {
		Init_declarator_listContext _localctx = new Init_declarator_listContext(_ctx, State);
		EnterRule(_localctx, 124, RULE_init_declarator_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 667; init_declarator();
			State = 672;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==31) {
				{
				{
				State = 668; Match(31);
				State = 669; init_declarator();
				}
				}
				State = 674;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Init_declaratorContext : ParserRuleContext {
		public DeclaratorContext declarator() {
			return GetRuleContext<DeclaratorContext>(0);
		}
		public InitializerContext initializer() {
			return GetRuleContext<InitializerContext>(0);
		}
		public Init_declaratorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_init_declarator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterInit_declarator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitInit_declarator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInit_declarator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Init_declaratorContext init_declarator() {
		Init_declaratorContext _localctx = new Init_declaratorContext(_ctx, State);
		EnterRule(_localctx, 126, RULE_init_declarator);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 675; declarator();
			State = 678;
			_la = _input.La(1);
			if (_la==85) {
				{
				State = 676; Match(85);
				State = 677; initializer();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Struct_or_union_specifierContext : ParserRuleContext {
		public Struct_declarationContext[] struct_declaration() {
			return GetRuleContexts<Struct_declarationContext>();
		}
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public Struct_declarationContext struct_declaration(int i) {
			return GetRuleContext<Struct_declarationContext>(i);
		}
		public Struct_or_union_specifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_struct_or_union_specifier; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterStruct_or_union_specifier(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitStruct_or_union_specifier(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStruct_or_union_specifier(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Struct_or_union_specifierContext struct_or_union_specifier() {
		Struct_or_union_specifierContext _localctx = new Struct_or_union_specifierContext(_ctx, State);
		EnterRule(_localctx, 128, RULE_struct_or_union_specifier);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 680;
			_la = _input.La(1);
			if ( !(_la==47 || _la==82) ) {
			_errHandler.RecoverInline(this);
			}
			Consume();
			State = 693;
			switch ( Interpreter.AdaptivePredict(_input,61,_ctx) ) {
			case 1:
				{
				State = 681; Match(IDENTIFIER);
				}
				break;

			case 2:
				{
				State = 683;
				_la = _input.La(1);
				if (_la==IDENTIFIER) {
					{
					State = 682; Match(IDENTIFIER);
					}
				}

				State = 685; Match(98);
				State = 687;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 686; struct_declaration();
					}
					}
					State = 689;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0) );
				State = 691; Match(7);
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Struct_declarationContext : ParserRuleContext {
		public Struct_declarator_listContext struct_declarator_list() {
			return GetRuleContext<Struct_declarator_listContext>(0);
		}
		public Specifier_qualifier_listContext specifier_qualifier_list() {
			return GetRuleContext<Specifier_qualifier_listContext>(0);
		}
		public Struct_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_struct_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterStruct_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitStruct_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStruct_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Struct_declarationContext struct_declaration() {
		Struct_declarationContext _localctx = new Struct_declarationContext(_ctx, State);
		EnterRule(_localctx, 130, RULE_struct_declaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 695; specifier_qualifier_list();
			State = 696; struct_declarator_list();
			State = 697; Match(108);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Specifier_qualifier_listContext : ParserRuleContext {
		public Type_qualifierContext type_qualifier(int i) {
			return GetRuleContext<Type_qualifierContext>(i);
		}
		public Type_specifierContext type_specifier(int i) {
			return GetRuleContext<Type_specifierContext>(i);
		}
		public Type_specifierContext[] type_specifier() {
			return GetRuleContexts<Type_specifierContext>();
		}
		public Type_qualifierContext[] type_qualifier() {
			return GetRuleContexts<Type_qualifierContext>();
		}
		public Specifier_qualifier_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_specifier_qualifier_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterSpecifier_qualifier_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitSpecifier_qualifier_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSpecifier_qualifier_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Specifier_qualifier_listContext specifier_qualifier_list() {
		Specifier_qualifier_listContext _localctx = new Specifier_qualifier_listContext(_ctx, State);
		EnterRule(_localctx, 132, RULE_specifier_qualifier_list);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 701;
			_errHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(_input,63,_ctx);
			do {
				switch (_alt) {
				case 1:
					{
					State = 701;
					switch (_input.La(1)) {
					case 5:
					case 8:
					case 9:
					case 16:
					case 25:
					case 28:
					case 36:
					case 38:
					case 47:
					case 71:
					case 82:
					case 83:
					case 88:
					case IDENTIFIER:
						{
						State = 699; type_specifier();
						}
						break;
					case 6:
					case 13:
					case 20:
					case 30:
					case 39:
					case 60:
					case 86:
					case 109:
						{
						State = 700; type_qualifier();
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				State = 703;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,63,_ctx);
			} while ( _alt!=2 && _alt!=-1 );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Struct_declarator_listContext : ParserRuleContext {
		public Struct_declaratorContext struct_declarator(int i) {
			return GetRuleContext<Struct_declaratorContext>(i);
		}
		public Struct_declaratorContext[] struct_declarator() {
			return GetRuleContexts<Struct_declaratorContext>();
		}
		public Struct_declarator_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_struct_declarator_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterStruct_declarator_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitStruct_declarator_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStruct_declarator_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Struct_declarator_listContext struct_declarator_list() {
		Struct_declarator_listContext _localctx = new Struct_declarator_listContext(_ctx, State);
		EnterRule(_localctx, 134, RULE_struct_declarator_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 705; struct_declarator();
			State = 710;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==31) {
				{
				{
				State = 706; Match(31);
				State = 707; struct_declarator();
				}
				}
				State = 712;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Struct_declaratorContext : ParserRuleContext {
		public DeclaratorContext declarator() {
			return GetRuleContext<DeclaratorContext>(0);
		}
		public ConstantContext constant() {
			return GetRuleContext<ConstantContext>(0);
		}
		public Struct_declaratorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_struct_declarator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterStruct_declarator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitStruct_declarator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStruct_declarator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Struct_declaratorContext struct_declarator() {
		Struct_declaratorContext _localctx = new Struct_declaratorContext(_ctx, State);
		EnterRule(_localctx, 136, RULE_struct_declarator);
		int _la;
		try {
			State = 719;
			switch ( Interpreter.AdaptivePredict(_input,66,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 713; declarator();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 715;
				_la = _input.La(1);
				if (_la==3 || _la==95 || _la==IDENTIFIER) {
					{
					State = 714; declarator();
					}
				}

				State = 717; Match(94);
				State = 718; constant();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Enum_specifierContext : ParserRuleContext {
		public Enumerator_listContext enumerator_list() {
			return GetRuleContext<Enumerator_listContext>(0);
		}
		public IdentifierContext identifier() {
			return GetRuleContext<IdentifierContext>(0);
		}
		public Enum_specifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_enum_specifier; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterEnum_specifier(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitEnum_specifier(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEnum_specifier(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Enum_specifierContext enum_specifier() {
		Enum_specifierContext _localctx = new Enum_specifierContext(_ctx, State);
		EnterRule(_localctx, 138, RULE_enum_specifier);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 721; Match(88);
			State = 733;
			switch (_input.La(1)) {
			case IDENTIFIER:
				{
				State = 722; identifier();
				State = 727;
				switch ( Interpreter.AdaptivePredict(_input,67,_ctx) ) {
				case 1:
					{
					State = 723; Match(98);
					State = 724; enumerator_list();
					State = 725; Match(7);
					}
					break;
				}
				}
				break;
			case 98:
				{
				State = 729; Match(98);
				State = 730; enumerator_list();
				State = 731; Match(7);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Enumerator_listContext : ParserRuleContext {
		public EnumeratorContext enumerator(int i) {
			return GetRuleContext<EnumeratorContext>(i);
		}
		public EnumeratorContext[] enumerator() {
			return GetRuleContexts<EnumeratorContext>();
		}
		public Enumerator_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_enumerator_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterEnumerator_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitEnumerator_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEnumerator_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Enumerator_listContext enumerator_list() {
		Enumerator_listContext _localctx = new Enumerator_listContext(_ctx, State);
		EnterRule(_localctx, 140, RULE_enumerator_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 735; enumerator();
			State = 740;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==31) {
				{
				{
				State = 736; Match(31);
				State = 737; enumerator();
				}
				}
				State = 742;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class EnumeratorContext : ParserRuleContext {
		public Constant_expressionContext constant_expression() {
			return GetRuleContext<Constant_expressionContext>(0);
		}
		public IdentifierContext identifier() {
			return GetRuleContext<IdentifierContext>(0);
		}
		public EnumeratorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_enumerator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterEnumerator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitEnumerator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEnumerator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public EnumeratorContext enumerator() {
		EnumeratorContext _localctx = new EnumeratorContext(_ctx, State);
		EnterRule(_localctx, 142, RULE_enumerator);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 743; identifier();
			State = 746;
			_la = _input.La(1);
			if (_la==85) {
				{
				State = 744; Match(85);
				State = 745; constant_expression();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class DeclaratorContext : ParserRuleContext {
		public Direct_declaratorContext direct_declarator() {
			return GetRuleContext<Direct_declaratorContext>(0);
		}
		public DeclaratorContext declarator() {
			return GetRuleContext<DeclaratorContext>(0);
		}
		public Type_qualifierContext type_qualifier(int i) {
			return GetRuleContext<Type_qualifierContext>(i);
		}
		public Type_qualifierContext[] type_qualifier() {
			return GetRuleContexts<Type_qualifierContext>();
		}
		public DeclaratorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_declarator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterDeclarator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitDeclarator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDeclarator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public DeclaratorContext declarator() {
		DeclaratorContext _localctx = new DeclaratorContext(_ctx, State);
		EnterRule(_localctx, 144, RULE_declarator);
		int _la;
		try {
			State = 757;
			switch (_input.La(1)) {
			case 3:
				EnterOuterAlt(_localctx, 1);
				{
				State = 748; Match(3);
				State = 752;
				_errHandler.Sync(this);
				_la = _input.La(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 6) | (1L << 13) | (1L << 20) | (1L << 30) | (1L << 39) | (1L << 60))) != 0) || _la==86 || _la==109) {
					{
					{
					State = 749; type_qualifier();
					}
					}
					State = 754;
					_errHandler.Sync(this);
					_la = _input.La(1);
				}
				State = 755; declarator();
				}
				break;
			case 95:
			case IDENTIFIER:
				EnterOuterAlt(_localctx, 2);
				{
				State = 756; direct_declarator();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Direct_declaratorContext : ParserRuleContext {
		public Declarator_suffixContext declarator_suffix(int i) {
			return GetRuleContext<Declarator_suffixContext>(i);
		}
		public DeclaratorContext declarator() {
			return GetRuleContext<DeclaratorContext>(0);
		}
		public Declarator_suffixContext[] declarator_suffix() {
			return GetRuleContexts<Declarator_suffixContext>();
		}
		public IdentifierContext identifier() {
			return GetRuleContext<IdentifierContext>(0);
		}
		public Direct_declaratorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_direct_declarator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterDirect_declarator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitDirect_declarator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDirect_declarator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Direct_declaratorContext direct_declarator() {
		Direct_declaratorContext _localctx = new Direct_declaratorContext(_ctx, State);
		EnterRule(_localctx, 146, RULE_direct_declarator);
		try {
			int _alt;
			State = 775;
			switch (_input.La(1)) {
			case IDENTIFIER:
				EnterOuterAlt(_localctx, 1);
				{
				State = 759; identifier();
				State = 763;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,73,_ctx);
				while ( _alt!=2 && _alt!=-1 ) {
					if ( _alt==1 ) {
						{
						{
						State = 760; declarator_suffix();
						}
						} 
					}
					State = 765;
					_errHandler.Sync(this);
					_alt = Interpreter.AdaptivePredict(_input,73,_ctx);
				}
				}
				break;
			case 95:
				EnterOuterAlt(_localctx, 2);
				{
				State = 766; Match(95);
				State = 767; declarator();
				State = 768; Match(14);
				State = 772;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,74,_ctx);
				while ( _alt!=2 && _alt!=-1 ) {
					if ( _alt==1 ) {
						{
						{
						State = 769; declarator_suffix();
						}
						} 
					}
					State = 774;
					_errHandler.Sync(this);
					_alt = Interpreter.AdaptivePredict(_input,74,_ctx);
				}
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Declarator_suffixContext : ParserRuleContext {
		public Constant_expressionContext constant_expression() {
			return GetRuleContext<Constant_expressionContext>(0);
		}
		public Parameter_listContext parameter_list() {
			return GetRuleContext<Parameter_listContext>(0);
		}
		public Declarator_suffixContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_declarator_suffix; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterDeclarator_suffix(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitDeclarator_suffix(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitDeclarator_suffix(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Declarator_suffixContext declarator_suffix() {
		Declarator_suffixContext _localctx = new Declarator_suffixContext(_ctx, State);
		EnterRule(_localctx, 148, RULE_declarator_suffix);
		int _la;
		try {
			State = 787;
			switch (_input.La(1)) {
			case 63:
				EnterOuterAlt(_localctx, 1);
				{
				State = 777; Match(63);
				State = 779;
				_la = _input.La(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 1) | (1L << 3) | (1L << 22) | (1L << 24) | (1L << 26) | (1L << 34) | (1L << 48) | (1L << 59) | (1L << 62) | (1L << 63))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (67 - 67)) | (1L << (73 - 67)) | (1L << (74 - 67)) | (1L << (95 - 67)) | (1L << (IDENTIFIER - 67)) | (1L << (CHARACTER_LITERAL - 67)) | (1L << (STRING_LITERAL - 67)) | (1L << (HEX_LITERAL - 67)) | (1L << (DECIMAL_LITERAL - 67)) | (1L << (OCTAL_LITERAL - 67)) | (1L << (FLOATING_POINT_LITERAL - 67)))) != 0)) {
					{
					State = 778; constant_expression();
					}
				}

				State = 781; Match(91);
				}
				break;
			case 95:
				EnterOuterAlt(_localctx, 2);
				{
				State = 782; Match(95);
				State = 784;
				_la = _input.La(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (100 - 71)) | (1L << (107 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0)) {
					{
					State = 783; parameter_list();
					}
				}

				State = 786; Match(14);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Parameter_listContext : ParserRuleContext {
		public Parameter_declaration_listContext parameter_declaration_list() {
			return GetRuleContext<Parameter_declaration_listContext>(0);
		}
		public Parameter_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_parameter_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterParameter_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitParameter_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitParameter_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Parameter_listContext parameter_list() {
		Parameter_listContext _localctx = new Parameter_listContext(_ctx, State);
		EnterRule(_localctx, 150, RULE_parameter_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 789; parameter_declaration_list();
			State = 792;
			_la = _input.La(1);
			if (_la==31) {
				{
				State = 790; Match(31);
				State = 791; Match(41);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Parameter_declarationContext : ParserRuleContext {
		public Declaration_specifiersContext declaration_specifiers() {
			return GetRuleContext<Declaration_specifiersContext>(0);
		}
		public DeclaratorContext declarator() {
			return GetRuleContext<DeclaratorContext>(0);
		}
		public Abstract_declaratorContext abstract_declarator() {
			return GetRuleContext<Abstract_declaratorContext>(0);
		}
		public Parameter_declarationContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_parameter_declaration; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterParameter_declaration(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitParameter_declaration(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitParameter_declaration(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Parameter_declarationContext parameter_declaration() {
		Parameter_declarationContext _localctx = new Parameter_declarationContext(_ctx, State);
		EnterRule(_localctx, 152, RULE_parameter_declaration);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 794; declaration_specifiers();
			State = 799;
			switch ( Interpreter.AdaptivePredict(_input,81,_ctx) ) {
			case 1:
				{
				State = 796;
				switch ( Interpreter.AdaptivePredict(_input,80,_ctx) ) {
				case 1:
					{
					State = 795; declarator();
					}
					break;
				}
				}
				break;

			case 2:
				{
				State = 798; abstract_declarator();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class InitializerContext : ParserRuleContext {
		public InitializerContext initializer(int i) {
			return GetRuleContext<InitializerContext>(i);
		}
		public Assignment_expressionContext assignment_expression() {
			return GetRuleContext<Assignment_expressionContext>(0);
		}
		public InitializerContext[] initializer() {
			return GetRuleContexts<InitializerContext>();
		}
		public InitializerContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_initializer; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterInitializer(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitInitializer(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInitializer(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public InitializerContext initializer() {
		InitializerContext _localctx = new InitializerContext(_ctx, State);
		EnterRule(_localctx, 154, RULE_initializer);
		int _la;
		try {
			State = 813;
			switch (_input.La(1)) {
			case 1:
			case 3:
			case 22:
			case 24:
			case 26:
			case 34:
			case 48:
			case 59:
			case 62:
			case 63:
			case 67:
			case 73:
			case 74:
			case 95:
			case IDENTIFIER:
			case CHARACTER_LITERAL:
			case STRING_LITERAL:
			case HEX_LITERAL:
			case DECIMAL_LITERAL:
			case OCTAL_LITERAL:
			case FLOATING_POINT_LITERAL:
				EnterOuterAlt(_localctx, 1);
				{
				State = 801; assignment_expression();
				}
				break;
			case 98:
				EnterOuterAlt(_localctx, 2);
				{
				State = 802; Match(98);
				State = 803; initializer();
				State = 808;
				_errHandler.Sync(this);
				_la = _input.La(1);
				while (_la==31) {
					{
					{
					State = 804; Match(31);
					State = 805; initializer();
					}
					}
					State = 810;
					_errHandler.Sync(this);
					_la = _input.La(1);
				}
				State = 811; Match(7);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Type_nameContext : ParserRuleContext {
		public Abstract_declaratorContext abstract_declarator() {
			return GetRuleContext<Abstract_declaratorContext>(0);
		}
		public Specifier_qualifier_listContext specifier_qualifier_list() {
			return GetRuleContext<Specifier_qualifier_listContext>(0);
		}
		public Type_nameContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_type_name; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterType_name(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitType_name(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitType_name(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Type_nameContext type_name() {
		Type_nameContext _localctx = new Type_nameContext(_ctx, State);
		EnterRule(_localctx, 156, RULE_type_name);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 815; specifier_qualifier_list();
			State = 816; abstract_declarator();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Abstract_declaratorContext : ParserRuleContext {
		public Constant_expressionContext constant_expression(int i) {
			return GetRuleContext<Constant_expressionContext>(i);
		}
		public Abstract_declaratorContext abstract_declarator() {
			return GetRuleContext<Abstract_declaratorContext>(0);
		}
		public Abstract_declarator_suffixContext abstract_declarator_suffix(int i) {
			return GetRuleContext<Abstract_declarator_suffixContext>(i);
		}
		public Type_qualifierContext type_qualifier(int i) {
			return GetRuleContext<Type_qualifierContext>(i);
		}
		public Constant_expressionContext[] constant_expression() {
			return GetRuleContexts<Constant_expressionContext>();
		}
		public Abstract_declarator_suffixContext[] abstract_declarator_suffix() {
			return GetRuleContexts<Abstract_declarator_suffixContext>();
		}
		public Type_qualifierContext[] type_qualifier() {
			return GetRuleContexts<Type_qualifierContext>();
		}
		public Abstract_declaratorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_abstract_declarator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterAbstract_declarator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitAbstract_declarator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAbstract_declarator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Abstract_declaratorContext abstract_declarator() {
		Abstract_declaratorContext _localctx = new Abstract_declaratorContext(_ctx, State);
		EnterRule(_localctx, 158, RULE_abstract_declarator);
		int _la;
		try {
			int _alt;
			State = 844;
			switch ( Interpreter.AdaptivePredict(_input,88,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 818; Match(3);
				State = 822;
				_errHandler.Sync(this);
				_la = _input.La(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 6) | (1L << 13) | (1L << 20) | (1L << 30) | (1L << 39) | (1L << 60))) != 0) || _la==86 || _la==109) {
					{
					{
					State = 819; type_qualifier();
					}
					}
					State = 824;
					_errHandler.Sync(this);
					_la = _input.La(1);
				}
				State = 825; abstract_declarator();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 826; Match(95);
				State = 827; abstract_declarator();
				State = 828; Match(14);
				State = 830;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,85,_ctx);
				do {
					switch (_alt) {
					case 1:
						{
						{
						State = 829; abstract_declarator_suffix();
						}
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					State = 832;
					_errHandler.Sync(this);
					_alt = Interpreter.AdaptivePredict(_input,85,_ctx);
				} while ( _alt!=2 && _alt!=-1 );
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 839;
				_errHandler.Sync(this);
				_la = _input.La(1);
				do {
					{
					{
					State = 834; Match(63);
					State = 836;
					_la = _input.La(1);
					if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 1) | (1L << 3) | (1L << 22) | (1L << 24) | (1L << 26) | (1L << 34) | (1L << 48) | (1L << 59) | (1L << 62) | (1L << 63))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (67 - 67)) | (1L << (73 - 67)) | (1L << (74 - 67)) | (1L << (95 - 67)) | (1L << (IDENTIFIER - 67)) | (1L << (CHARACTER_LITERAL - 67)) | (1L << (STRING_LITERAL - 67)) | (1L << (HEX_LITERAL - 67)) | (1L << (DECIMAL_LITERAL - 67)) | (1L << (OCTAL_LITERAL - 67)) | (1L << (FLOATING_POINT_LITERAL - 67)))) != 0)) {
						{
						State = 835; constant_expression();
						}
					}

					State = 838; Match(91);
					}
					}
					State = 841;
					_errHandler.Sync(this);
					_la = _input.La(1);
				} while ( _la==63 );
				}
				break;

			case 4:
				EnterOuterAlt(_localctx, 4);
				{
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Abstract_declarator_suffixContext : ParserRuleContext {
		public Parameter_declaration_listContext parameter_declaration_list() {
			return GetRuleContext<Parameter_declaration_listContext>(0);
		}
		public Constant_expressionContext constant_expression() {
			return GetRuleContext<Constant_expressionContext>(0);
		}
		public Abstract_declarator_suffixContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_abstract_declarator_suffix; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterAbstract_declarator_suffix(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitAbstract_declarator_suffix(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAbstract_declarator_suffix(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Abstract_declarator_suffixContext abstract_declarator_suffix() {
		Abstract_declarator_suffixContext _localctx = new Abstract_declarator_suffixContext(_ctx, State);
		EnterRule(_localctx, 160, RULE_abstract_declarator_suffix);
		int _la;
		try {
			State = 856;
			switch (_input.La(1)) {
			case 63:
				EnterOuterAlt(_localctx, 1);
				{
				State = 846; Match(63);
				State = 848;
				_la = _input.La(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 1) | (1L << 3) | (1L << 22) | (1L << 24) | (1L << 26) | (1L << 34) | (1L << 48) | (1L << 59) | (1L << 62) | (1L << 63))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (67 - 67)) | (1L << (73 - 67)) | (1L << (74 - 67)) | (1L << (95 - 67)) | (1L << (IDENTIFIER - 67)) | (1L << (CHARACTER_LITERAL - 67)) | (1L << (STRING_LITERAL - 67)) | (1L << (HEX_LITERAL - 67)) | (1L << (DECIMAL_LITERAL - 67)) | (1L << (OCTAL_LITERAL - 67)) | (1L << (FLOATING_POINT_LITERAL - 67)))) != 0)) {
					{
					State = 847; constant_expression();
					}
				}

				State = 850; Match(91);
				}
				break;
			case 95:
				EnterOuterAlt(_localctx, 2);
				{
				State = 851; Match(95);
				State = 853;
				_la = _input.La(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 2) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 20) | (1L << 25) | (1L << 28) | (1L << 30) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 47) | (1L << 49) | (1L << 60))) != 0) || ((((_la - 71)) & ~0x3f) == 0 && ((1L << (_la - 71)) & ((1L << (71 - 71)) | (1L << (82 - 71)) | (1L << (83 - 71)) | (1L << (86 - 71)) | (1L << (88 - 71)) | (1L << (100 - 71)) | (1L << (107 - 71)) | (1L << (109 - 71)) | (1L << (IDENTIFIER - 71)))) != 0)) {
					{
					State = 852; parameter_declaration_list();
					}
				}

				State = 855; Match(14);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Parameter_declaration_listContext : ParserRuleContext {
		public Parameter_declarationContext[] parameter_declaration() {
			return GetRuleContexts<Parameter_declarationContext>();
		}
		public Parameter_declarationContext parameter_declaration(int i) {
			return GetRuleContext<Parameter_declarationContext>(i);
		}
		public Parameter_declaration_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_parameter_declaration_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterParameter_declaration_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitParameter_declaration_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitParameter_declaration_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Parameter_declaration_listContext parameter_declaration_list() {
		Parameter_declaration_listContext _localctx = new Parameter_declaration_listContext(_ctx, State);
		EnterRule(_localctx, 162, RULE_parameter_declaration_list);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 858; parameter_declaration();
			State = 863;
			_errHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(_input,92,_ctx);
			while ( _alt!=2 && _alt!=-1 ) {
				if ( _alt==1 ) {
					{
					{
					State = 859; Match(31);
					State = 860; parameter_declaration();
					}
					} 
				}
				State = 865;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,92,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Statement_listContext : ParserRuleContext {
		public StatementContext[] statement() {
			return GetRuleContexts<StatementContext>();
		}
		public StatementContext statement(int i) {
			return GetRuleContext<StatementContext>(i);
		}
		public Statement_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_statement_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterStatement_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitStatement_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStatement_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Statement_listContext statement_list() {
		Statement_listContext _localctx = new Statement_listContext(_ctx, State);
		EnterRule(_localctx, 164, RULE_statement_list);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 867;
			_errHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(_input,93,_ctx);
			do {
				switch (_alt) {
				case 1:
					{
					{
					State = 866; statement();
					}
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				State = 869;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,93,_ctx);
			} while ( _alt!=2 && _alt!=-1 );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class StatementContext : ParserRuleContext {
		public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public Selection_statementContext selection_statement() {
			return GetRuleContext<Selection_statementContext>(0);
		}
		public Labeled_statementContext labeled_statement() {
			return GetRuleContext<Labeled_statementContext>(0);
		}
		public Iteration_statementContext iteration_statement() {
			return GetRuleContext<Iteration_statementContext>(0);
		}
		public Jump_statementContext jump_statement() {
			return GetRuleContext<Jump_statementContext>(0);
		}
		public Compound_statementContext compound_statement() {
			return GetRuleContext<Compound_statementContext>(0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterStatement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitStatement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStatement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public StatementContext statement() {
		StatementContext _localctx = new StatementContext(_ctx, State);
		EnterRule(_localctx, 166, RULE_statement);
		try {
			State = 880;
			switch ( Interpreter.AdaptivePredict(_input,94,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 871; labeled_statement();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 872; expression();
				State = 873; Match(108);
				}
				break;

			case 3:
				EnterOuterAlt(_localctx, 3);
				{
				State = 875; compound_statement();
				}
				break;

			case 4:
				EnterOuterAlt(_localctx, 4);
				{
				State = 876; selection_statement();
				}
				break;

			case 5:
				EnterOuterAlt(_localctx, 5);
				{
				State = 877; iteration_statement();
				}
				break;

			case 6:
				EnterOuterAlt(_localctx, 6);
				{
				State = 878; jump_statement();
				}
				break;

			case 7:
				EnterOuterAlt(_localctx, 7);
				{
				State = 879; Match(108);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Labeled_statementContext : ParserRuleContext {
		public StatementContext statement() {
			return GetRuleContext<StatementContext>(0);
		}
		public Constant_expressionContext constant_expression() {
			return GetRuleContext<Constant_expressionContext>(0);
		}
		public IdentifierContext identifier() {
			return GetRuleContext<IdentifierContext>(0);
		}
		public Labeled_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_labeled_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterLabeled_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitLabeled_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitLabeled_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Labeled_statementContext labeled_statement() {
		Labeled_statementContext _localctx = new Labeled_statementContext(_ctx, State);
		EnterRule(_localctx, 168, RULE_labeled_statement);
		try {
			State = 894;
			switch (_input.La(1)) {
			case IDENTIFIER:
				EnterOuterAlt(_localctx, 1);
				{
				State = 882; identifier();
				State = 883; Match(94);
				State = 884; statement();
				}
				break;
			case 75:
				EnterOuterAlt(_localctx, 2);
				{
				State = 886; Match(75);
				State = 887; constant_expression();
				State = 888; Match(94);
				State = 889; statement();
				}
				break;
			case 92:
				EnterOuterAlt(_localctx, 3);
				{
				State = 891; Match(92);
				State = 892; Match(94);
				State = 893; statement();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Compound_statementContext : ParserRuleContext {
		public DeclarationContext[] declaration() {
			return GetRuleContexts<DeclarationContext>();
		}
		public Statement_listContext[] statement_list() {
			return GetRuleContexts<Statement_listContext>();
		}
		public DeclarationContext declaration(int i) {
			return GetRuleContext<DeclarationContext>(i);
		}
		public Statement_listContext statement_list(int i) {
			return GetRuleContext<Statement_listContext>(i);
		}
		public Compound_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_compound_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterCompound_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitCompound_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCompound_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Compound_statementContext compound_statement() {
		Compound_statementContext _localctx = new Compound_statementContext(_ctx, State);
		EnterRule(_localctx, 170, RULE_compound_statement);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 896; Match(98);
			State = 901;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 1) | (1L << 2) | (1L << 3) | (1L << 5) | (1L << 6) | (1L << 8) | (1L << 9) | (1L << 10) | (1L << 11) | (1L << 13) | (1L << 16) | (1L << 18) | (1L << 20) | (1L << 22) | (1L << 24) | (1L << 25) | (1L << 26) | (1L << 28) | (1L << 30) | (1L << 32) | (1L << 34) | (1L << 35) | (1L << 36) | (1L << 38) | (1L << 39) | (1L << 42) | (1L << 47) | (1L << 48) | (1L << 49) | (1L << 57) | (1L << 59) | (1L << 60) | (1L << 62) | (1L << 63))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (67 - 67)) | (1L << (68 - 67)) | (1L << (71 - 67)) | (1L << (73 - 67)) | (1L << (74 - 67)) | (1L << (75 - 67)) | (1L << (82 - 67)) | (1L << (83 - 67)) | (1L << (86 - 67)) | (1L << (88 - 67)) | (1L << (92 - 67)) | (1L << (95 - 67)) | (1L << (98 - 67)) | (1L << (100 - 67)) | (1L << (105 - 67)) | (1L << (106 - 67)) | (1L << (107 - 67)) | (1L << (108 - 67)) | (1L << (109 - 67)) | (1L << (IDENTIFIER - 67)) | (1L << (CHARACTER_LITERAL - 67)) | (1L << (STRING_LITERAL - 67)) | (1L << (HEX_LITERAL - 67)) | (1L << (DECIMAL_LITERAL - 67)) | (1L << (OCTAL_LITERAL - 67)) | (1L << (FLOATING_POINT_LITERAL - 67)))) != 0)) {
				{
				State = 899;
				switch ( Interpreter.AdaptivePredict(_input,96,_ctx) ) {
				case 1:
					{
					State = 897; declaration();
					}
					break;

				case 2:
					{
					State = 898; statement_list();
					}
					break;
				}
				}
				State = 903;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			State = 904; Match(7);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Selection_statementContext : ParserRuleContext {
		public StatementContext[] statement() {
			return GetRuleContexts<StatementContext>();
		}
		public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public StatementContext statement(int i) {
			return GetRuleContext<StatementContext>(i);
		}
		public Selection_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_selection_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterSelection_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitSelection_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitSelection_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Selection_statementContext selection_statement() {
		Selection_statementContext _localctx = new Selection_statementContext(_ctx, State);
		EnterRule(_localctx, 172, RULE_selection_statement);
		try {
			State = 921;
			switch (_input.La(1)) {
			case 35:
				EnterOuterAlt(_localctx, 1);
				{
				State = 906; Match(35);
				State = 907; Match(95);
				State = 908; expression();
				State = 909; Match(14);
				State = 910; statement();
				State = 913;
				switch ( Interpreter.AdaptivePredict(_input,98,_ctx) ) {
				case 1:
					{
					State = 911; Match(45);
					State = 912; statement();
					}
					break;
				}
				}
				break;
			case 57:
				EnterOuterAlt(_localctx, 2);
				{
				State = 915; Match(57);
				State = 916; Match(95);
				State = 917; expression();
				State = 918; Match(14);
				State = 919; statement();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Iteration_statementContext : ParserRuleContext {
		public StatementContext statement() {
			return GetRuleContext<StatementContext>(0);
		}
		public ExpressionContext[] expression() {
			return GetRuleContexts<ExpressionContext>();
		}
		public ExpressionContext expression(int i) {
			return GetRuleContext<ExpressionContext>(i);
		}
		public Iteration_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_iteration_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterIteration_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitIteration_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitIteration_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Iteration_statementContext iteration_statement() {
		Iteration_statementContext _localctx = new Iteration_statementContext(_ctx, State);
		EnterRule(_localctx, 174, RULE_iteration_statement);
		int _la;
		try {
			State = 952;
			switch (_input.La(1)) {
			case 32:
				EnterOuterAlt(_localctx, 1);
				{
				State = 923; Match(32);
				State = 924; Match(95);
				State = 925; expression();
				State = 926; Match(14);
				State = 927; statement();
				}
				break;
			case 10:
				EnterOuterAlt(_localctx, 2);
				{
				State = 929; Match(10);
				State = 930; statement();
				State = 931; Match(32);
				State = 932; Match(95);
				State = 933; expression();
				State = 934; Match(14);
				State = 935; Match(108);
				}
				break;
			case 105:
				EnterOuterAlt(_localctx, 3);
				{
				State = 937; Match(105);
				State = 938; Match(95);
				State = 940;
				_la = _input.La(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 1) | (1L << 3) | (1L << 22) | (1L << 24) | (1L << 26) | (1L << 34) | (1L << 48) | (1L << 59) | (1L << 62) | (1L << 63))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (67 - 67)) | (1L << (73 - 67)) | (1L << (74 - 67)) | (1L << (95 - 67)) | (1L << (IDENTIFIER - 67)) | (1L << (CHARACTER_LITERAL - 67)) | (1L << (STRING_LITERAL - 67)) | (1L << (HEX_LITERAL - 67)) | (1L << (DECIMAL_LITERAL - 67)) | (1L << (OCTAL_LITERAL - 67)) | (1L << (FLOATING_POINT_LITERAL - 67)))) != 0)) {
					{
					State = 939; expression();
					}
				}

				State = 942; Match(108);
				State = 944;
				_la = _input.La(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 1) | (1L << 3) | (1L << 22) | (1L << 24) | (1L << 26) | (1L << 34) | (1L << 48) | (1L << 59) | (1L << 62) | (1L << 63))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (67 - 67)) | (1L << (73 - 67)) | (1L << (74 - 67)) | (1L << (95 - 67)) | (1L << (IDENTIFIER - 67)) | (1L << (CHARACTER_LITERAL - 67)) | (1L << (STRING_LITERAL - 67)) | (1L << (HEX_LITERAL - 67)) | (1L << (DECIMAL_LITERAL - 67)) | (1L << (OCTAL_LITERAL - 67)) | (1L << (FLOATING_POINT_LITERAL - 67)))) != 0)) {
					{
					State = 943; expression();
					}
				}

				State = 946; Match(108);
				State = 948;
				_la = _input.La(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 1) | (1L << 3) | (1L << 22) | (1L << 24) | (1L << 26) | (1L << 34) | (1L << 48) | (1L << 59) | (1L << 62) | (1L << 63))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (67 - 67)) | (1L << (73 - 67)) | (1L << (74 - 67)) | (1L << (95 - 67)) | (1L << (IDENTIFIER - 67)) | (1L << (CHARACTER_LITERAL - 67)) | (1L << (STRING_LITERAL - 67)) | (1L << (HEX_LITERAL - 67)) | (1L << (DECIMAL_LITERAL - 67)) | (1L << (OCTAL_LITERAL - 67)) | (1L << (FLOATING_POINT_LITERAL - 67)))) != 0)) {
					{
					State = 947; expression();
					}
				}

				State = 950; Match(14);
				State = 951; statement();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Jump_statementContext : ParserRuleContext {
		public ExpressionContext expression() {
			return GetRuleContext<ExpressionContext>(0);
		}
		public IdentifierContext identifier() {
			return GetRuleContext<IdentifierContext>(0);
		}
		public Jump_statementContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_jump_statement; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterJump_statement(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitJump_statement(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitJump_statement(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Jump_statementContext jump_statement() {
		Jump_statementContext _localctx = new Jump_statementContext(_ctx, State);
		EnterRule(_localctx, 176, RULE_jump_statement);
		int _la;
		try {
			State = 967;
			switch (_input.La(1)) {
			case 18:
				EnterOuterAlt(_localctx, 1);
				{
				State = 954; Match(18);
				State = 955; identifier();
				State = 956; Match(108);
				}
				break;
			case 68:
				EnterOuterAlt(_localctx, 2);
				{
				State = 958; Match(68);
				State = 959; Match(108);
				}
				break;
			case 42:
				EnterOuterAlt(_localctx, 3);
				{
				State = 960; Match(42);
				State = 961; Match(108);
				}
				break;
			case 106:
				EnterOuterAlt(_localctx, 4);
				{
				State = 962; Match(106);
				State = 964;
				_la = _input.La(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 1) | (1L << 3) | (1L << 22) | (1L << 24) | (1L << 26) | (1L << 34) | (1L << 48) | (1L << 59) | (1L << 62) | (1L << 63))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (67 - 67)) | (1L << (73 - 67)) | (1L << (74 - 67)) | (1L << (95 - 67)) | (1L << (IDENTIFIER - 67)) | (1L << (CHARACTER_LITERAL - 67)) | (1L << (STRING_LITERAL - 67)) | (1L << (HEX_LITERAL - 67)) | (1L << (DECIMAL_LITERAL - 67)) | (1L << (OCTAL_LITERAL - 67)) | (1L << (FLOATING_POINT_LITERAL - 67)))) != 0)) {
					{
					State = 963; expression();
					}
				}

				State = 966; Match(108);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExpressionContext : ParserRuleContext {
		public Assignment_expressionContext[] assignment_expression() {
			return GetRuleContexts<Assignment_expressionContext>();
		}
		public Assignment_expressionContext assignment_expression(int i) {
			return GetRuleContext<Assignment_expressionContext>(i);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterExpression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitExpression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExpression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExpressionContext expression() {
		ExpressionContext _localctx = new ExpressionContext(_ctx, State);
		EnterRule(_localctx, 178, RULE_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 969; assignment_expression();
			State = 974;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==31) {
				{
				{
				State = 970; Match(31);
				State = 971; assignment_expression();
				}
				}
				State = 976;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Assignment_expressionContext : ParserRuleContext {
		public Assignment_operatorContext assignment_operator() {
			return GetRuleContext<Assignment_operatorContext>(0);
		}
		public Conditional_expressionContext conditional_expression() {
			return GetRuleContext<Conditional_expressionContext>(0);
		}
		public Assignment_expressionContext assignment_expression() {
			return GetRuleContext<Assignment_expressionContext>(0);
		}
		public Assignment_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_assignment_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterAssignment_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitAssignment_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAssignment_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Assignment_expressionContext assignment_expression() {
		Assignment_expressionContext _localctx = new Assignment_expressionContext(_ctx, State);
		EnterRule(_localctx, 180, RULE_assignment_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 977; conditional_expression();
			State = 981;
			_la = _input.La(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 12) | (1L << 29) | (1L << 40) | (1L << 43) | (1L << 44) | (1L << 55) | (1L << 56))) != 0) || ((((_la - 85)) & ~0x3f) == 0 && ((1L << (_la - 85)) & ((1L << (85 - 85)) | (1L << (87 - 85)) | (1L << (90 - 85)) | (1L << (96 - 85)))) != 0)) {
				{
				State = 978; assignment_operator();
				State = 979; assignment_expression();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Assignment_operatorContext : ParserRuleContext {
		public Assignment_operatorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_assignment_operator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterAssignment_operator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitAssignment_operator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAssignment_operator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Assignment_operatorContext assignment_operator() {
		Assignment_operatorContext _localctx = new Assignment_operatorContext(_ctx, State);
		EnterRule(_localctx, 182, RULE_assignment_operator);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 983;
			_la = _input.La(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 12) | (1L << 29) | (1L << 40) | (1L << 43) | (1L << 44) | (1L << 55) | (1L << 56))) != 0) || ((((_la - 85)) & ~0x3f) == 0 && ((1L << (_la - 85)) & ((1L << (85 - 85)) | (1L << (87 - 85)) | (1L << (90 - 85)) | (1L << (96 - 85)))) != 0)) ) {
			_errHandler.RecoverInline(this);
			}
			Consume();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Conditional_expressionContext : ParserRuleContext {
		public Logical_or_expressionContext[] logical_or_expression() {
			return GetRuleContexts<Logical_or_expressionContext>();
		}
		public Logical_or_expressionContext logical_or_expression(int i) {
			return GetRuleContext<Logical_or_expressionContext>(i);
		}
		public Conditional_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_conditional_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterConditional_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitConditional_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitConditional_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Conditional_expressionContext conditional_expression() {
		Conditional_expressionContext _localctx = new Conditional_expressionContext(_ctx, State);
		EnterRule(_localctx, 184, RULE_conditional_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 985; logical_or_expression();
			State = 991;
			_la = _input.La(1);
			if (_la==37) {
				{
				State = 986; Match(37);
				State = 987; logical_or_expression();
				State = 988; Match(94);
				State = 989; logical_or_expression();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Constant_expressionContext : ParserRuleContext {
		public Conditional_expressionContext conditional_expression() {
			return GetRuleContext<Conditional_expressionContext>(0);
		}
		public Constant_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_constant_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterConstant_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitConstant_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitConstant_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Constant_expressionContext constant_expression() {
		Constant_expressionContext _localctx = new Constant_expressionContext(_ctx, State);
		EnterRule(_localctx, 186, RULE_constant_expression);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 993; conditional_expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Logical_or_expressionContext : ParserRuleContext {
		public Logical_and_expressionContext[] logical_and_expression() {
			return GetRuleContexts<Logical_and_expressionContext>();
		}
		public Logical_and_expressionContext logical_and_expression(int i) {
			return GetRuleContext<Logical_and_expressionContext>(i);
		}
		public Logical_or_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_logical_or_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterLogical_or_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitLogical_or_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitLogical_or_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Logical_or_expressionContext logical_or_expression() {
		Logical_or_expressionContext _localctx = new Logical_or_expressionContext(_ctx, State);
		EnterRule(_localctx, 188, RULE_logical_or_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 995; logical_and_expression();
			State = 1000;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==53) {
				{
				{
				State = 996; Match(53);
				State = 997; logical_and_expression();
				}
				}
				State = 1002;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Logical_and_expressionContext : ParserRuleContext {
		public Inclusive_or_expressionContext[] inclusive_or_expression() {
			return GetRuleContexts<Inclusive_or_expressionContext>();
		}
		public Inclusive_or_expressionContext inclusive_or_expression(int i) {
			return GetRuleContext<Inclusive_or_expressionContext>(i);
		}
		public Logical_and_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_logical_and_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterLogical_and_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitLogical_and_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitLogical_and_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Logical_and_expressionContext logical_and_expression() {
		Logical_and_expressionContext _localctx = new Logical_and_expressionContext(_ctx, State);
		EnterRule(_localctx, 190, RULE_logical_and_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1003; inclusive_or_expression();
			State = 1008;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==52) {
				{
				{
				State = 1004; Match(52);
				State = 1005; inclusive_or_expression();
				}
				}
				State = 1010;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Inclusive_or_expressionContext : ParserRuleContext {
		public Exclusive_or_expressionContext exclusive_or_expression(int i) {
			return GetRuleContext<Exclusive_or_expressionContext>(i);
		}
		public Exclusive_or_expressionContext[] exclusive_or_expression() {
			return GetRuleContexts<Exclusive_or_expressionContext>();
		}
		public Inclusive_or_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_inclusive_or_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterInclusive_or_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitInclusive_or_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitInclusive_or_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Inclusive_or_expressionContext inclusive_or_expression() {
		Inclusive_or_expressionContext _localctx = new Inclusive_or_expressionContext(_ctx, State);
		EnterRule(_localctx, 192, RULE_inclusive_or_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1011; exclusive_or_expression();
			State = 1016;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==23) {
				{
				{
				State = 1012; Match(23);
				State = 1013; exclusive_or_expression();
				}
				}
				State = 1018;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Exclusive_or_expressionContext : ParserRuleContext {
		public And_expressionContext[] and_expression() {
			return GetRuleContexts<And_expressionContext>();
		}
		public And_expressionContext and_expression(int i) {
			return GetRuleContext<And_expressionContext>(i);
		}
		public Exclusive_or_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_exclusive_or_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterExclusive_or_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitExclusive_or_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExclusive_or_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Exclusive_or_expressionContext exclusive_or_expression() {
		Exclusive_or_expressionContext _localctx = new Exclusive_or_expressionContext(_ctx, State);
		EnterRule(_localctx, 194, RULE_exclusive_or_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1019; and_expression();
			State = 1024;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==103) {
				{
				{
				State = 1020; Match(103);
				State = 1021; and_expression();
				}
				}
				State = 1026;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class And_expressionContext : ParserRuleContext {
		public Equality_expressionContext equality_expression(int i) {
			return GetRuleContext<Equality_expressionContext>(i);
		}
		public Equality_expressionContext[] equality_expression() {
			return GetRuleContexts<Equality_expressionContext>();
		}
		public And_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_and_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterAnd_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitAnd_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAnd_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public And_expressionContext and_expression() {
		And_expressionContext _localctx = new And_expressionContext(_ctx, State);
		EnterRule(_localctx, 196, RULE_and_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1027; equality_expression();
			State = 1032;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==62) {
				{
				{
				State = 1028; Match(62);
				State = 1029; equality_expression();
				}
				}
				State = 1034;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Equality_expressionContext : ParserRuleContext {
		public Relational_expressionContext relational_expression(int i) {
			return GetRuleContext<Relational_expressionContext>(i);
		}
		public Relational_expressionContext[] relational_expression() {
			return GetRuleContexts<Relational_expressionContext>();
		}
		public Equality_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_equality_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterEquality_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitEquality_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitEquality_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Equality_expressionContext equality_expression() {
		Equality_expressionContext _localctx = new Equality_expressionContext(_ctx, State);
		EnterRule(_localctx, 198, RULE_equality_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1035; relational_expression();
			State = 1040;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==69 || _la==111) {
				{
				{
				State = 1036;
				_la = _input.La(1);
				if ( !(_la==69 || _la==111) ) {
				_errHandler.RecoverInline(this);
				}
				Consume();
				State = 1037; relational_expression();
				}
				}
				State = 1042;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Relational_expressionContext : ParserRuleContext {
		public Shift_expressionContext[] shift_expression() {
			return GetRuleContexts<Shift_expressionContext>();
		}
		public Shift_expressionContext shift_expression(int i) {
			return GetRuleContext<Shift_expressionContext>(i);
		}
		public Relational_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_relational_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterRelational_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitRelational_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitRelational_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Relational_expressionContext relational_expression() {
		Relational_expressionContext _localctx = new Relational_expressionContext(_ctx, State);
		EnterRule(_localctx, 200, RULE_relational_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1043; shift_expression();
			State = 1048;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (((((_la - 54)) & ~0x3f) == 0 && ((1L << (_la - 54)) & ((1L << (54 - 54)) | (1L << (66 - 54)) | (1L << (70 - 54)) | (1L << (113 - 54)))) != 0)) {
				{
				{
				State = 1044;
				_la = _input.La(1);
				if ( !(((((_la - 54)) & ~0x3f) == 0 && ((1L << (_la - 54)) & ((1L << (54 - 54)) | (1L << (66 - 54)) | (1L << (70 - 54)) | (1L << (113 - 54)))) != 0)) ) {
				_errHandler.RecoverInline(this);
				}
				Consume();
				State = 1045; shift_expression();
				}
				}
				State = 1050;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Shift_expressionContext : ParserRuleContext {
		public Additive_expressionContext[] additive_expression() {
			return GetRuleContexts<Additive_expressionContext>();
		}
		public Additive_expressionContext additive_expression(int i) {
			return GetRuleContext<Additive_expressionContext>(i);
		}
		public Shift_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_shift_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterShift_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitShift_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitShift_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Shift_expressionContext shift_expression() {
		Shift_expressionContext _localctx = new Shift_expressionContext(_ctx, State);
		EnterRule(_localctx, 202, RULE_shift_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1051; additive_expression();
			State = 1056;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==72 || _la==102) {
				{
				{
				State = 1052;
				_la = _input.La(1);
				if ( !(_la==72 || _la==102) ) {
				_errHandler.RecoverInline(this);
				}
				Consume();
				State = 1053; additive_expression();
				}
				}
				State = 1058;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Additive_expressionContext : ParserRuleContext {
		public Multiplicative_expressionContext multiplicative_expression(int i) {
			return GetRuleContext<Multiplicative_expressionContext>(i);
		}
		public Multiplicative_expressionContext[] multiplicative_expression() {
			return GetRuleContexts<Multiplicative_expressionContext>();
		}
		public Additive_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_additive_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterAdditive_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitAdditive_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAdditive_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Additive_expressionContext additive_expression() {
		Additive_expressionContext _localctx = new Additive_expressionContext(_ctx, State);
		EnterRule(_localctx, 204, RULE_additive_expression);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1059; multiplicative_expression();
			State = 1064;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==34 || _la==51) {
				{
				{
				State = 1060;
				_la = _input.La(1);
				if ( !(_la==34 || _la==51) ) {
				_errHandler.RecoverInline(this);
				}
				Consume();
				State = 1061; multiplicative_expression();
				}
				}
				State = 1066;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Multiplicative_expressionContext : ParserRuleContext {
		public Cast_expressionContext[] cast_expression() {
			return GetRuleContexts<Cast_expressionContext>();
		}
		public Cast_expressionContext cast_expression(int i) {
			return GetRuleContext<Cast_expressionContext>(i);
		}
		public Multiplicative_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_multiplicative_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterMultiplicative_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitMultiplicative_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitMultiplicative_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Multiplicative_expressionContext multiplicative_expression() {
		Multiplicative_expressionContext _localctx = new Multiplicative_expressionContext(_ctx, State);
		EnterRule(_localctx, 206, RULE_multiplicative_expression);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 1067; cast_expression();
			State = 1072;
			_errHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(_input,118,_ctx);
			while ( _alt!=2 && _alt!=-1 ) {
				if ( _alt==1 ) {
					{
					{
					State = 1068;
					_la = _input.La(1);
					if ( !(_la==3 || _la==58 || _la==80) ) {
					_errHandler.RecoverInline(this);
					}
					Consume();
					State = 1069; cast_expression();
					}
					} 
				}
				State = 1074;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,118,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Cast_expressionContext : ParserRuleContext {
		public Cast_expressionContext cast_expression() {
			return GetRuleContext<Cast_expressionContext>(0);
		}
		public Unary_expressionContext unary_expression() {
			return GetRuleContext<Unary_expressionContext>(0);
		}
		public Type_nameContext type_name() {
			return GetRuleContext<Type_nameContext>(0);
		}
		public Cast_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_cast_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterCast_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitCast_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCast_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Cast_expressionContext cast_expression() {
		Cast_expressionContext _localctx = new Cast_expressionContext(_ctx, State);
		EnterRule(_localctx, 208, RULE_cast_expression);
		try {
			State = 1081;
			switch ( Interpreter.AdaptivePredict(_input,119,_ctx) ) {
			case 1:
				EnterOuterAlt(_localctx, 1);
				{
				State = 1075; Match(95);
				State = 1076; type_name();
				State = 1077; Match(14);
				State = 1078; cast_expression();
				}
				break;

			case 2:
				EnterOuterAlt(_localctx, 2);
				{
				State = 1080; unary_expression();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Unary_expressionContext : ParserRuleContext {
		public Cast_expressionContext cast_expression() {
			return GetRuleContext<Cast_expressionContext>(0);
		}
		public Postfix_expressionContext postfix_expression() {
			return GetRuleContext<Postfix_expressionContext>(0);
		}
		public Unary_operatorContext unary_operator() {
			return GetRuleContext<Unary_operatorContext>(0);
		}
		public Unary_expressionContext unary_expression() {
			return GetRuleContext<Unary_expressionContext>(0);
		}
		public Type_nameContext type_name() {
			return GetRuleContext<Type_nameContext>(0);
		}
		public Unary_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_unary_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterUnary_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitUnary_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitUnary_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Unary_expressionContext unary_expression() {
		Unary_expressionContext _localctx = new Unary_expressionContext(_ctx, State);
		EnterRule(_localctx, 210, RULE_unary_expression);
		try {
			State = 1099;
			switch (_input.La(1)) {
			case 1:
			case 22:
			case 63:
			case 73:
			case 74:
			case 95:
			case IDENTIFIER:
			case CHARACTER_LITERAL:
			case STRING_LITERAL:
			case HEX_LITERAL:
			case DECIMAL_LITERAL:
			case OCTAL_LITERAL:
			case FLOATING_POINT_LITERAL:
				EnterOuterAlt(_localctx, 1);
				{
				State = 1083; postfix_expression();
				}
				break;
			case 48:
				EnterOuterAlt(_localctx, 2);
				{
				State = 1084; Match(48);
				State = 1085; unary_expression();
				}
				break;
			case 67:
				EnterOuterAlt(_localctx, 3);
				{
				State = 1086; Match(67);
				State = 1087; unary_expression();
				}
				break;
			case 3:
			case 24:
			case 34:
			case 59:
			case 62:
				EnterOuterAlt(_localctx, 4);
				{
				State = 1088; unary_operator();
				State = 1089; cast_expression();
				}
				break;
			case 26:
				EnterOuterAlt(_localctx, 5);
				{
				State = 1091; Match(26);
				State = 1097;
				switch ( Interpreter.AdaptivePredict(_input,120,_ctx) ) {
				case 1:
					{
					State = 1092; Match(95);
					State = 1093; type_name();
					State = 1094; Match(14);
					}
					break;

				case 2:
					{
					State = 1096; unary_expression();
					}
					break;
				}
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Unary_operatorContext : ParserRuleContext {
		public Unary_operatorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_unary_operator; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterUnary_operator(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitUnary_operator(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitUnary_operator(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Unary_operatorContext unary_operator() {
		Unary_operatorContext _localctx = new Unary_operatorContext(_ctx, State);
		EnterRule(_localctx, 212, RULE_unary_operator);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1101;
			_la = _input.La(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 3) | (1L << 24) | (1L << 34) | (1L << 59) | (1L << 62))) != 0)) ) {
			_errHandler.RecoverInline(this);
			}
			Consume();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Postfix_expressionContext : ParserRuleContext {
		public ExpressionContext[] expression() {
			return GetRuleContexts<ExpressionContext>();
		}
		public Argument_expression_listContext argument_expression_list(int i) {
			return GetRuleContext<Argument_expression_listContext>(i);
		}
		public IdentifierContext identifier(int i) {
			return GetRuleContext<IdentifierContext>(i);
		}
		public ExpressionContext expression(int i) {
			return GetRuleContext<ExpressionContext>(i);
		}
		public Primary_expressionContext primary_expression() {
			return GetRuleContext<Primary_expressionContext>(0);
		}
		public IdentifierContext[] identifier() {
			return GetRuleContexts<IdentifierContext>();
		}
		public Argument_expression_listContext[] argument_expression_list() {
			return GetRuleContexts<Argument_expression_listContext>();
		}
		public Postfix_expressionContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_postfix_expression; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterPostfix_expression(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitPostfix_expression(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitPostfix_expression(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Postfix_expressionContext postfix_expression() {
		Postfix_expressionContext _localctx = new Postfix_expressionContext(_ctx, State);
		EnterRule(_localctx, 214, RULE_postfix_expression);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 1103; primary_expression();
			State = 1121;
			_errHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(_input,124,_ctx);
			while ( _alt!=2 && _alt!=-1 ) {
				if ( _alt==1 ) {
					{
					State = 1119;
					switch (_input.La(1)) {
					case 63:
						{
						State = 1104; Match(63);
						State = 1105; expression();
						State = 1106; Match(91);
						}
						break;
					case 95:
						{
						State = 1108; Match(95);
						State = 1110;
						_la = _input.La(1);
						if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << 1) | (1L << 3) | (1L << 22) | (1L << 24) | (1L << 26) | (1L << 34) | (1L << 48) | (1L << 59) | (1L << 62) | (1L << 63))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (67 - 67)) | (1L << (73 - 67)) | (1L << (74 - 67)) | (1L << (95 - 67)) | (1L << (IDENTIFIER - 67)) | (1L << (CHARACTER_LITERAL - 67)) | (1L << (STRING_LITERAL - 67)) | (1L << (HEX_LITERAL - 67)) | (1L << (DECIMAL_LITERAL - 67)) | (1L << (OCTAL_LITERAL - 67)) | (1L << (FLOATING_POINT_LITERAL - 67)))) != 0)) {
							{
							State = 1109; argument_expression_list();
							}
						}

						State = 1112; Match(14);
						}
						break;
					case 50:
						{
						State = 1113; Match(50);
						State = 1114; identifier();
						}
						break;
					case 81:
						{
						State = 1115; Match(81);
						State = 1116; identifier();
						}
						break;
					case 48:
						{
						State = 1117; Match(48);
						}
						break;
					case 67:
						{
						State = 1118; Match(67);
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					} 
				}
				State = 1123;
				_errHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(_input,124,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class Argument_expression_listContext : ParserRuleContext {
		public Assignment_expressionContext[] assignment_expression() {
			return GetRuleContexts<Assignment_expressionContext>();
		}
		public Assignment_expressionContext assignment_expression(int i) {
			return GetRuleContext<Assignment_expressionContext>(i);
		}
		public Argument_expression_listContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_argument_expression_list; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterArgument_expression_list(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitArgument_expression_list(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitArgument_expression_list(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public Argument_expression_listContext argument_expression_list() {
		Argument_expression_listContext _localctx = new Argument_expression_listContext(_ctx, State);
		EnterRule(_localctx, 216, RULE_argument_expression_list);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1124; assignment_expression();
			State = 1129;
			_errHandler.Sync(this);
			_la = _input.La(1);
			while (_la==31) {
				{
				{
				State = 1125; Match(31);
				State = 1126; assignment_expression();
				}
				}
				State = 1131;
				_errHandler.Sync(this);
				_la = _input.La(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class IdentifierContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER() { return GetToken(ObjCParser.IDENTIFIER, 0); }
		public IdentifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_identifier; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterIdentifier(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitIdentifier(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitIdentifier(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public IdentifierContext identifier() {
		IdentifierContext _localctx = new IdentifierContext(_ctx, State);
		EnterRule(_localctx, 218, RULE_identifier);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1132; Match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ConstantContext : ParserRuleContext {
		public ITerminalNode CHARACTER_LITERAL() { return GetToken(ObjCParser.CHARACTER_LITERAL, 0); }
		public ITerminalNode OCTAL_LITERAL() { return GetToken(ObjCParser.OCTAL_LITERAL, 0); }
		public ITerminalNode HEX_LITERAL() { return GetToken(ObjCParser.HEX_LITERAL, 0); }
		public ITerminalNode FLOATING_POINT_LITERAL() { return GetToken(ObjCParser.FLOATING_POINT_LITERAL, 0); }
		public ITerminalNode DECIMAL_LITERAL() { return GetToken(ObjCParser.DECIMAL_LITERAL, 0); }
		public ConstantContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int GetRuleIndex() { return RULE_constant; }
		public override void EnterRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.EnterConstant(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IObjCListener typedListener = listener as IObjCListener;
			if (typedListener != null) typedListener.ExitConstant(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IObjCVisitor<TResult> typedVisitor = visitor as IObjCVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitConstant(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ConstantContext constant() {
		ConstantContext _localctx = new ConstantContext(_ctx, State);
		EnterRule(_localctx, 220, RULE_constant);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 1134;
			_la = _input.La(1);
			if ( !(((((_la - 115)) & ~0x3f) == 0 && ((1L << (_la - 115)) & ((1L << (CHARACTER_LITERAL - 115)) | (1L << (HEX_LITERAL - 115)) | (1L << (DECIMAL_LITERAL - 115)) | (1L << (OCTAL_LITERAL - 115)) | (1L << (FLOATING_POINT_LITERAL - 115)))) != 0)) ) {
			_errHandler.RecoverInline(this);
			}
			Consume();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.ReportError(this, re);
			_errHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public static readonly string _serializedATN =
		"\x5\x3\x80\x473\x4\x2\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6"+
		"\x4\a\t\a\x4\b\t\b\x4\t\t\t\x4\n\t\n\x4\v\t\v\x4\f\t\f\x4\r\t\r\x4\xE"+
		"\t\xE\x4\xF\t\xF\x4\x10\t\x10\x4\x11\t\x11\x4\x12\t\x12\x4\x13\t\x13\x4"+
		"\x14\t\x14\x4\x15\t\x15\x4\x16\t\x16\x4\x17\t\x17\x4\x18\t\x18\x4\x19"+
		"\t\x19\x4\x1A\t\x1A\x4\x1B\t\x1B\x4\x1C\t\x1C\x4\x1D\t\x1D\x4\x1E\t\x1E"+
		"\x4\x1F\t\x1F\x4 \t \x4!\t!\x4\"\t\"\x4#\t#\x4$\t$\x4%\t%\x4&\t&\x4\'"+
		"\t\'\x4(\t(\x4)\t)\x4*\t*\x4+\t+\x4,\t,\x4-\t-\x4.\t.\x4/\t/\x4\x30\t"+
		"\x30\x4\x31\t\x31\x4\x32\t\x32\x4\x33\t\x33\x4\x34\t\x34\x4\x35\t\x35"+
		"\x4\x36\t\x36\x4\x37\t\x37\x4\x38\t\x38\x4\x39\t\x39\x4:\t:\x4;\t;\x4"+
		"<\t<\x4=\t=\x4>\t>\x4?\t?\x4@\t@\x4\x41\t\x41\x4\x42\t\x42\x4\x43\t\x43"+
		"\x4\x44\t\x44\x4\x45\t\x45\x4\x46\t\x46\x4G\tG\x4H\tH\x4I\tI\x4J\tJ\x4"+
		"K\tK\x4L\tL\x4M\tM\x4N\tN\x4O\tO\x4P\tP\x4Q\tQ\x4R\tR\x4S\tS\x4T\tT\x4"+
		"U\tU\x4V\tV\x4W\tW\x4X\tX\x4Y\tY\x4Z\tZ\x4[\t[\x4\\\t\\\x4]\t]\x4^\t^"+
		"\x4_\t_\x4`\t`\x4\x61\t\x61\x4\x62\t\x62\x4\x63\t\x63\x4\x64\t\x64\x4"+
		"\x65\t\x65\x4\x66\t\x66\x4g\tg\x4h\th\x4i\ti\x4j\tj\x4k\tk\x4l\tl\x4m"+
		"\tm\x4n\tn\x4o\to\x4p\tp\x3\x2\x6\x2\xE2\n\x2\r\x2\xE\x2\xE3\x3\x2\x3"+
		"\x2\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3\x3"+
		"\x3\x3\x5\x3\xF4\n\x3\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4"+
		"\x3\x4\x3\x4\x3\x4\x3\x4\x3\x4\x5\x4\x103\n\x4\x3\x5\x3\x5\x3\x6\x3\x6"+
		"\x3\x6\x3\x6\x5\x6\x10B\n\x6\x3\x6\x5\x6\x10E\n\x6\x3\x6\x5\x6\x111\n"+
		"\x6\x3\x6\x5\x6\x114\n\x6\x3\x6\x3\x6\x3\a\x3\a\x3\a\x3\a\x5\a\x11C\n"+
		"\a\x3\a\x3\a\x5\a\x120\n\a\x3\a\x5\a\x123\n\a\x3\a\x3\a\x3\b\x3\b\x3\b"+
		"\x3\b\x5\b\x12B\n\b\x3\b\x5\b\x12E\n\b\x3\b\x3\b\x3\t\x3\t\x3\t\x3\t\x3"+
		"\t\x3\t\x5\t\x138\n\t\x3\t\x3\t\x3\n\x3\n\x3\n\x5\n\x13F\n\n\x3\n\x5\n"+
		"\x142\n\n\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x3\f\x3\f\x3\f\x3\f\x3\r\x3\r"+
		"\x3\r\a\r\x151\n\r\f\r\xE\r\x154\v\r\x3\xE\x3\xE\x3\xE\x3\xE\x3\xF\x3"+
		"\xF\x3\xF\a\xF\x15D\n\xF\f\xF\xE\xF\x160\v\xF\x3\x10\x3\x10\x5\x10\x164"+
		"\n\x10\x3\x10\x3\x10\x3\x11\x3\x11\x3\x11\x3\x11\x3\x12\x3\x12\x3\x12"+
		"\a\x12\x16F\n\x12\f\x12\xE\x12\x172\v\x12\x3\x13\x3\x13\x3\x13\x3\x13"+
		"\x3\x13\x3\x13\x3\x13\x3\x13\x5\x13\x17C\n\x13\x3\x14\x3\x14\x3\x15\x3"+
		"\x15\x3\x16\x3\x16\x3\x17\x3\x17\x3\x18\x3\x18\a\x18\x188\n\x18\f\x18"+
		"\xE\x18\x18B\v\x18\x3\x18\x3\x18\x3\x18\x3\x18\x6\x18\x191\n\x18\r\x18"+
		"\xE\x18\x192\x3\x18\x3\x18\x3\x18\x3\x18\x6\x18\x199\n\x18\r\x18\xE\x18"+
		"\x19A\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x3\x18\x6\x18\x1A3\n\x18\r\x18"+
		"\xE\x18\x1A4\x3\x18\x3\x18\x3\x18\x5\x18\x1AA\n\x18\x3\x19\x3\x19\x3\x1A"+
		"\x3\x1A\x3\x1A\x3\x1A\x6\x1A\x1B2\n\x1A\r\x1A\xE\x1A\x1B3\x3\x1B\x3\x1B"+
		"\x3\x1B\x3\x1C\x3\x1C\x3\x1C\x3\x1D\x5\x1D\x1BD\n\x1D\x3\x1D\x3\x1D\x3"+
		"\x1D\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x3\x1E\x6\x1E\x1C7\n\x1E\r\x1E\xE\x1E"+
		"\x1C8\x3\x1F\x3\x1F\x3\x1F\x3 \x3 \x3 \x3!\x5!\x1D2\n!\x3!\x3!\x5!\x1D6"+
		"\n!\x3!\x3!\x3\"\x3\"\x6\"\x1DC\n\"\r\"\xE\"\x1DD\x3\"\x5\"\x1E1\n\"\x5"+
		"\"\x1E3\n\"\x3#\x5#\x1E6\n#\x3#\x3#\a#\x1EA\n#\f#\xE#\x1ED\v#\x3#\x3#"+
		"\x3$\x3$\x3%\x3%\x3%\x3%\x3&\x3&\x3&\x3&\x3&\x3&\x3&\x3&\x5&\x1FF\n&\x3"+
		"\'\x3\'\x3\'\a\'\x204\n\'\f\'\xE\'\x207\v\'\x3(\x3(\x3(\x3(\x5(\x20D\n"+
		"(\x3)\x3)\x3)\x3)\x3)\x3)\x3)\x3)\x3)\x3)\x3)\x5)\x21A\n)\x3)\x3)\x5)"+
		"\x21E\n)\x3)\x3)\x3)\x5)\x223\n)\x3*\x3*\x3*\x5*\x228\n*\x3+\x3+\x3,\x3"+
		",\x3,\x3,\x3,\x3,\x3,\x3,\x3,\x3,\x3,\x3,\x5,\x238\n,\x3-\x3-\x3-\x3-"+
		"\x3-\x3.\x3.\x3.\x5.\x242\n.\x3/\x3/\x6/\x246\n/\r/\xE/\x247\x5/\x24A"+
		"\n/\x3\x30\x5\x30\x24D\n\x30\x3\x30\x3\x30\x3\x30\x3\x31\x3\x31\x3\x31"+
		"\x3\x31\x3\x31\x3\x32\x3\x32\x5\x32\x259\n\x32\x3\x32\x6\x32\x25C\n\x32"+
		"\r\x32\xE\x32\x25D\x5\x32\x260\n\x32\x3\x33\x3\x33\x3\x33\x3\x33\x3\x33"+
		"\x3\x34\x3\x34\x3\x34\x3\x34\x3\x34\x3\x35\x3\x35\x3\x36\x3\x36\x3\x37"+
		"\x3\x37\x3\x37\x3\x37\x3\x37\x3\x37\x3\x38\x3\x38\x3\x38\x3\x39\x3\x39"+
		"\x3\x39\x3\x39\x3\x39\x3:\x3:\x3:\x5:\x281\n:\x3;\x3;\x3;\x3;\x3;\x3;"+
		"\x3<\x5<\x28A\n<\x3<\x3<\x3<\x3=\x3=\x5=\x291\n=\x3=\x3=\x3>\x3>\x3>\x6"+
		">\x298\n>\r>\xE>\x299\x3?\x3?\x3@\x3@\x3@\a@\x2A1\n@\f@\xE@\x2A4\v@\x3"+
		"\x41\x3\x41\x3\x41\x5\x41\x2A9\n\x41\x3\x42\x3\x42\x3\x42\x5\x42\x2AE"+
		"\n\x42\x3\x42\x3\x42\x6\x42\x2B2\n\x42\r\x42\xE\x42\x2B3\x3\x42\x3\x42"+
		"\x5\x42\x2B8\n\x42\x3\x43\x3\x43\x3\x43\x3\x43\x3\x44\x3\x44\x6\x44\x2C0"+
		"\n\x44\r\x44\xE\x44\x2C1\x3\x45\x3\x45\x3\x45\a\x45\x2C7\n\x45\f\x45\xE"+
		"\x45\x2CA\v\x45\x3\x46\x3\x46\x5\x46\x2CE\n\x46\x3\x46\x3\x46\x5\x46\x2D2"+
		"\n\x46\x3G\x3G\x3G\x3G\x3G\x3G\x5G\x2DA\nG\x3G\x3G\x3G\x3G\x5G\x2E0\n"+
		"G\x3H\x3H\x3H\aH\x2E5\nH\fH\xEH\x2E8\vH\x3I\x3I\x3I\x5I\x2ED\nI\x3J\x3"+
		"J\aJ\x2F1\nJ\fJ\xEJ\x2F4\vJ\x3J\x3J\x5J\x2F8\nJ\x3K\x3K\aK\x2FC\nK\fK"+
		"\xEK\x2FF\vK\x3K\x3K\x3K\x3K\aK\x305\nK\fK\xEK\x308\vK\x5K\x30A\nK\x3"+
		"L\x3L\x5L\x30E\nL\x3L\x3L\x3L\x5L\x313\nL\x3L\x5L\x316\nL\x3M\x3M\x3M"+
		"\x5M\x31B\nM\x3N\x3N\x5N\x31F\nN\x3N\x5N\x322\nN\x3O\x3O\x3O\x3O\x3O\a"+
		"O\x329\nO\fO\xEO\x32C\vO\x3O\x3O\x5O\x330\nO\x3P\x3P\x3P\x3Q\x3Q\aQ\x337"+
		"\nQ\fQ\xEQ\x33A\vQ\x3Q\x3Q\x3Q\x3Q\x3Q\x6Q\x341\nQ\rQ\xEQ\x342\x3Q\x3"+
		"Q\x5Q\x347\nQ\x3Q\x6Q\x34A\nQ\rQ\xEQ\x34B\x3Q\x5Q\x34F\nQ\x3R\x3R\x5R"+
		"\x353\nR\x3R\x3R\x3R\x5R\x358\nR\x3R\x5R\x35B\nR\x3S\x3S\x3S\aS\x360\n"+
		"S\fS\xES\x363\vS\x3T\x6T\x366\nT\rT\xET\x367\x3U\x3U\x3U\x3U\x3U\x3U\x3"+
		"U\x3U\x3U\x5U\x373\nU\x3V\x3V\x3V\x3V\x3V\x3V\x3V\x3V\x3V\x3V\x3V\x3V"+
		"\x5V\x381\nV\x3W\x3W\x3W\aW\x386\nW\fW\xEW\x389\vW\x3W\x3W\x3X\x3X\x3"+
		"X\x3X\x3X\x3X\x3X\x5X\x394\nX\x3X\x3X\x3X\x3X\x3X\x3X\x5X\x39C\nX\x3Y"+
		"\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x3Y\x5Y\x3AF"+
		"\nY\x3Y\x3Y\x5Y\x3B3\nY\x3Y\x3Y\x5Y\x3B7\nY\x3Y\x3Y\x5Y\x3BB\nY\x3Z\x3"+
		"Z\x3Z\x3Z\x3Z\x3Z\x3Z\x3Z\x3Z\x3Z\x5Z\x3C7\nZ\x3Z\x5Z\x3CA\nZ\x3[\x3["+
		"\x3[\a[\x3CF\n[\f[\xE[\x3D2\v[\x3\\\x3\\\x3\\\x3\\\x5\\\x3D8\n\\\x3]\x3"+
		"]\x3^\x3^\x3^\x3^\x3^\x3^\x5^\x3E2\n^\x3_\x3_\x3`\x3`\x3`\a`\x3E9\n`\f"+
		"`\xE`\x3EC\v`\x3\x61\x3\x61\x3\x61\a\x61\x3F1\n\x61\f\x61\xE\x61\x3F4"+
		"\v\x61\x3\x62\x3\x62\x3\x62\a\x62\x3F9\n\x62\f\x62\xE\x62\x3FC\v\x62\x3"+
		"\x63\x3\x63\x3\x63\a\x63\x401\n\x63\f\x63\xE\x63\x404\v\x63\x3\x64\x3"+
		"\x64\x3\x64\a\x64\x409\n\x64\f\x64\xE\x64\x40C\v\x64\x3\x65\x3\x65\x3"+
		"\x65\a\x65\x411\n\x65\f\x65\xE\x65\x414\v\x65\x3\x66\x3\x66\x3\x66\a\x66"+
		"\x419\n\x66\f\x66\xE\x66\x41C\v\x66\x3g\x3g\x3g\ag\x421\ng\fg\xEg\x424"+
		"\vg\x3h\x3h\x3h\ah\x429\nh\fh\xEh\x42C\vh\x3i\x3i\x3i\ai\x431\ni\fi\xE"+
		"i\x434\vi\x3j\x3j\x3j\x3j\x3j\x3j\x5j\x43C\nj\x3k\x3k\x3k\x3k\x3k\x3k"+
		"\x3k\x3k\x3k\x3k\x3k\x3k\x3k\x3k\x5k\x44C\nk\x5k\x44E\nk\x3l\x3l\x3m\x3"+
		"m\x3m\x3m\x3m\x3m\x3m\x5m\x459\nm\x3m\x3m\x3m\x3m\x3m\x3m\x3m\am\x462"+
		"\nm\fm\xEm\x465\vm\x3n\x3n\x3n\an\x46A\nn\fn\xEn\x46D\vn\x3o\x3o\x3p\x3"+
		"p\x3p\x2\x2\x2q\x2\x2\x4\x2\x6\x2\b\x2\n\x2\f\x2\xE\x2\x10\x2\x12\x2\x14"+
		"\x2\x16\x2\x18\x2\x1A\x2\x1C\x2\x1E\x2 \x2\"\x2$\x2&\x2(\x2*\x2,\x2.\x2"+
		"\x30\x2\x32\x2\x34\x2\x36\x2\x38\x2:\x2<\x2>\x2@\x2\x42\x2\x44\x2\x46"+
		"\x2H\x2J\x2L\x2N\x2P\x2R\x2T\x2V\x2X\x2Z\x2\\\x2^\x2`\x2\x62\x2\x64\x2"+
		"\x66\x2h\x2j\x2l\x2n\x2p\x2r\x2t\x2v\x2x\x2z\x2|\x2~\x2\x80\x2\x82\x2"+
		"\x84\x2\x86\x2\x88\x2\x8A\x2\x8C\x2\x8E\x2\x90\x2\x92\x2\x94\x2\x96\x2"+
		"\x98\x2\x9A\x2\x9C\x2\x9E\x2\xA0\x2\xA2\x2\xA4\x2\xA6\x2\xA8\x2\xAA\x2"+
		"\xAC\x2\xAE\x2\xB0\x2\xB2\x2\xB4\x2\xB6\x2\xB8\x2\xBA\x2\xBC\x2\xBE\x2"+
		"\xC0\x2\xC2\x2\xC4\x2\xC6\x2\xC8\x2\xCA\x2\xCC\x2\xCE\x2\xD0\x2\xD2\x2"+
		"\xD4\x2\xD6\x2\xD8\x2\xDA\x2\xDC\x2\xDE\x2\x2\xE\x6\x1D\x1DNN__jj\b\b"+
		"\b\xF\xF\x16\x16  ))>>\a\x4\x4\r\r\x33\x33\x66\x66mm\x4\x31\x31TT\v\xE"+
		"\xE\x1F\x1F**-.\x39:WWYY\\\\\x62\x62\x4GGqq\x6\x38\x38\x44\x44HHss\x4"+
		"JJhh\x4$$\x35\x35\x5\x5\x5<<RR\a\x5\x5\x1A\x1A$$==@@\x4uuwz\x4C1\x2\xE1"+
		"\x3\x2\x2\x2\x4\xF3\x3\x2\x2\x2\x6\x102\x3\x2\x2\x2\b\x104\x3\x2\x2\x2"+
		"\n\x106\x3\x2\x2\x2\f\x117\x3\x2\x2\x2\xE\x126\x3\x2\x2\x2\x10\x131\x3"+
		"\x2\x2\x2\x12\x13B\x3\x2\x2\x2\x14\x145\x3\x2\x2\x2\x16\x149\x3\x2\x2"+
		"\x2\x18\x14D\x3\x2\x2\x2\x1A\x155\x3\x2\x2\x2\x1C\x159\x3\x2\x2\x2\x1E"+
		"\x161\x3\x2\x2\x2 \x167\x3\x2\x2\x2\"\x16B\x3\x2\x2\x2$\x17B\x3\x2\x2"+
		"\x2&\x17D\x3\x2\x2\x2(\x17F\x3\x2\x2\x2*\x181\x3\x2\x2\x2,\x183\x3\x2"+
		"\x2\x2.\x1A9\x3\x2\x2\x2\x30\x1AB\x3\x2\x2\x2\x32\x1B1\x3\x2\x2\x2\x34"+
		"\x1B5\x3\x2\x2\x2\x36\x1B8\x3\x2\x2\x2\x38\x1BC\x3\x2\x2\x2:\x1C6\x3\x2"+
		"\x2\x2<\x1CA\x3\x2\x2\x2>\x1CD\x3\x2\x2\x2@\x1D1\x3\x2\x2\x2\x42\x1E2"+
		"\x3\x2\x2\x2\x44\x1E5\x3\x2\x2\x2\x46\x1F0\x3\x2\x2\x2H\x1F2\x3\x2\x2"+
		"\x2J\x1FE\x3\x2\x2\x2L\x200\x3\x2\x2\x2N\x20C\x3\x2\x2\x2P\x222\x3\x2"+
		"\x2\x2R\x227\x3\x2\x2\x2T\x229\x3\x2\x2\x2V\x237\x3\x2\x2\x2X\x239\x3"+
		"\x2\x2\x2Z\x241\x3\x2\x2\x2\\\x249\x3\x2\x2\x2^\x24C\x3\x2\x2\x2`\x251"+
		"\x3\x2\x2\x2\x62\x25F\x3\x2\x2\x2\x64\x261\x3\x2\x2\x2\x66\x266\x3\x2"+
		"\x2\x2h\x26B\x3\x2\x2\x2j\x26D\x3\x2\x2\x2l\x26F\x3\x2\x2\x2n\x275\x3"+
		"\x2\x2\x2p\x278\x3\x2\x2\x2r\x27D\x3\x2\x2\x2t\x282\x3\x2\x2\x2v\x289"+
		"\x3\x2\x2\x2x\x28E\x3\x2\x2\x2z\x297\x3\x2\x2\x2|\x29B\x3\x2\x2\x2~\x29D"+
		"\x3\x2\x2\x2\x80\x2A5\x3\x2\x2\x2\x82\x2AA\x3\x2\x2\x2\x84\x2B9\x3\x2"+
		"\x2\x2\x86\x2BF\x3\x2\x2\x2\x88\x2C3\x3\x2\x2\x2\x8A\x2D1\x3\x2\x2\x2"+
		"\x8C\x2D3\x3\x2\x2\x2\x8E\x2E1\x3\x2\x2\x2\x90\x2E9\x3\x2\x2\x2\x92\x2F7"+
		"\x3\x2\x2\x2\x94\x309\x3\x2\x2\x2\x96\x315\x3\x2\x2\x2\x98\x317\x3\x2"+
		"\x2\x2\x9A\x31C\x3\x2\x2\x2\x9C\x32F\x3\x2\x2\x2\x9E\x331\x3\x2\x2\x2"+
		"\xA0\x34E\x3\x2\x2\x2\xA2\x35A\x3\x2\x2\x2\xA4\x35C\x3\x2\x2\x2\xA6\x365"+
		"\x3\x2\x2\x2\xA8\x372\x3\x2\x2\x2\xAA\x380\x3\x2\x2\x2\xAC\x382\x3\x2"+
		"\x2\x2\xAE\x39B\x3\x2\x2\x2\xB0\x3BA\x3\x2\x2\x2\xB2\x3C9\x3\x2\x2\x2"+
		"\xB4\x3CB\x3\x2\x2\x2\xB6\x3D3\x3\x2\x2\x2\xB8\x3D9\x3\x2\x2\x2\xBA\x3DB"+
		"\x3\x2\x2\x2\xBC\x3E3\x3\x2\x2\x2\xBE\x3E5\x3\x2\x2\x2\xC0\x3ED\x3\x2"+
		"\x2\x2\xC2\x3F5\x3\x2\x2\x2\xC4\x3FD\x3\x2\x2\x2\xC6\x405\x3\x2\x2\x2"+
		"\xC8\x40D\x3\x2\x2\x2\xCA\x415\x3\x2\x2\x2\xCC\x41D\x3\x2\x2\x2\xCE\x425"+
		"\x3\x2\x2\x2\xD0\x42D\x3\x2\x2\x2\xD2\x43B\x3\x2\x2\x2\xD4\x44D\x3\x2"+
		"\x2\x2\xD6\x44F\x3\x2\x2\x2\xD8\x451\x3\x2\x2\x2\xDA\x466\x3\x2\x2\x2"+
		"\xDC\x46E\x3\x2\x2\x2\xDE\x470\x3\x2\x2\x2\xE0\xE2\x5\x4\x3\x2\xE1\xE0"+
		"\x3\x2\x2\x2\xE2\xE3\x3\x2\x2\x2\xE3\xE1\x3\x2\x2\x2\xE3\xE4\x3\x2\x2"+
		"\x2\xE4\xE5\x3\x2\x2\x2\xE5\xE6\a\x1\x2\x2\xE6\x3\x3\x2\x2\x2\xE7\xF4"+
		"\a\x7F\x2\x2\xE8\xF4\a\x80\x2\x2\xE9\xF4\x5\x6\x4\x2\xEA\xF4\x5v<\x2\xEB"+
		"\xF4\x5x=\x2\xEC\xF4\x5\n\x6\x2\xED\xF4\x5\xE\b\x2\xEE\xF4\x5\f\a\x2\xEF"+
		"\xF4\x5\x10\t\x2\xF0\xF4\x5\x12\n\x2\xF1\xF4\x5\x14\v\x2\xF2\xF4\x5\x16"+
		"\f\x2\xF3\xE7\x3\x2\x2\x2\xF3\xE8\x3\x2\x2\x2\xF3\xE9\x3\x2\x2\x2\xF3"+
		"\xEA\x3\x2\x2\x2\xF3\xEB\x3\x2\x2\x2\xF3\xEC\x3\x2\x2\x2\xF3\xED\x3\x2"+
		"\x2\x2\xF3\xEE\x3\x2\x2\x2\xF3\xEF\x3\x2\x2\x2\xF3\xF0\x3\x2\x2\x2\xF3"+
		"\xF1\x3\x2\x2\x2\xF3\xF2\x3\x2\x2\x2\xF4\x5\x3\x2\x2\x2\xF5\x103\a{\x2"+
		"\x2\xF6\x103\a|\x2\x2\xF7\xF8\ag\x2\x2\xF8\x103\x5\b\x5\x2\xF9\xFA\a\x17"+
		"\x2\x2\xFA\x103\x5\xB4[\x2\xFB\xFC\ar\x2\x2\xFC\x103\x5\xB4[\x2\xFD\xFE"+
		"\a\x65\x2\x2\xFE\x103\x5\xB4[\x2\xFF\x100\a\x42\x2\x2\x100\x103\x5\xB4"+
		"[\x2\x101\x103\a\x63\x2\x2\x102\xF5\x3\x2\x2\x2\x102\xF6\x3\x2\x2\x2\x102"+
		"\xF7\x3\x2\x2\x2\x102\xF9\x3\x2\x2\x2\x102\xFB\x3\x2\x2\x2\x102\xFD\x3"+
		"\x2\x2\x2\x102\xFF\x3\x2\x2\x2\x102\x101\x3\x2\x2\x2\x103\a\x3\x2\x2\x2"+
		"\x104\x105\a\x30\x2\x2\x105\t\x3\x2\x2\x2\x106\x107\ap\x2\x2\x107\x10A"+
		"\x5&\x14\x2\x108\x109\a`\x2\x2\x109\x10B\x5(\x15\x2\x10A\x108\x3\x2\x2"+
		"\x2\x10A\x10B\x3\x2\x2\x2\x10B\x10D\x3\x2\x2\x2\x10C\x10E\x5\x1A\xE\x2"+
		"\x10D\x10C\x3\x2\x2\x2\x10D\x10E\x3\x2\x2\x2\x10E\x110\x3\x2\x2\x2\x10F"+
		"\x111\x5.\x18\x2\x110\x10F\x3\x2\x2\x2\x110\x111\x3\x2\x2\x2\x111\x113"+
		"\x3\x2\x2\x2\x112\x114\x5\x32\x1A\x2\x113\x112\x3\x2\x2\x2\x113\x114\x3"+
		"\x2\x2\x2\x114\x115\x3\x2\x2\x2\x115\x116\a\x43\x2\x2\x116\v\x3\x2\x2"+
		"\x2\x117\x118\ap\x2\x2\x118\x119\x5&\x14\x2\x119\x11B\a\x61\x2\x2\x11A"+
		"\x11C\x5*\x16\x2\x11B\x11A\x3\x2\x2\x2\x11B\x11C\x3\x2\x2\x2\x11C\x11D"+
		"\x3\x2\x2\x2\x11D\x11F\a\x10\x2\x2\x11E\x120\x5\x1A\xE\x2\x11F\x11E\x3"+
		"\x2\x2\x2\x11F\x120\x3\x2\x2\x2\x120\x122\x3\x2\x2\x2\x121\x123\x5\x32"+
		"\x1A\x2\x122\x121\x3\x2\x2\x2\x122\x123\x3\x2\x2\x2\x123\x124\x3\x2\x2"+
		"\x2\x124\x125\a\x43\x2\x2\x125\r\x3\x2\x2\x2\x126\x127\a\x13\x2\x2\x127"+
		"\x12A\x5&\x14\x2\x128\x129\a`\x2\x2\x129\x12B\x5(\x15\x2\x12A\x128\x3"+
		"\x2\x2\x2\x12A\x12B\x3\x2\x2\x2\x12B\x12D\x3\x2\x2\x2\x12C\x12E\x5:\x1E"+
		"\x2\x12D\x12C\x3\x2\x2\x2\x12D\x12E\x3\x2\x2\x2\x12E\x12F\x3\x2\x2\x2"+
		"\x12F\x130\a\x43\x2\x2\x130\xF\x3\x2\x2\x2\x131\x132\a\x13\x2\x2\x132"+
		"\x133\x5&\x14\x2\x133\x134\a\x61\x2\x2\x134\x135\x5*\x16\x2\x135\x137"+
		"\a\x10\x2\x2\x136\x138\x5:\x1E\x2\x137\x136\x3\x2\x2\x2\x137\x138\x3\x2"+
		"\x2\x2\x138\x139\x3\x2\x2\x2\x139\x13A\a\x43\x2\x2\x13A\x11\x3\x2\x2\x2"+
		"\x13B\x13C\aL\x2\x2\x13C\x13E\x5,\x17\x2\x13D\x13F\x5\x1A\xE\x2\x13E\x13D"+
		"\x3\x2\x2\x2\x13E\x13F\x3\x2\x2\x2\x13F\x141\x3\x2\x2\x2\x140\x142\x5"+
		"\x32\x1A\x2\x141\x140\x3\x2\x2\x2\x141\x142\x3\x2\x2\x2\x142\x143\x3\x2"+
		"\x2\x2\x143\x144\a\x43\x2\x2\x144\x13\x3\x2\x2\x2\x145\x146\aL\x2\x2\x146"+
		"\x147\x5\x1C\xF\x2\x147\x148\an\x2\x2\x148\x15\x3\x2\x2\x2\x149\x14A\a"+
		"[\x2\x2\x14A\x14B\x5\x18\r\x2\x14B\x14C\an\x2\x2\x14C\x17\x3\x2\x2\x2"+
		"\x14D\x152\x5&\x14\x2\x14E\x14F\a!\x2\x2\x14F\x151\x5&\x14\x2\x150\x14E"+
		"\x3\x2\x2\x2\x151\x154\x3\x2\x2\x2\x152\x150\x3\x2\x2\x2\x152\x153\x3"+
		"\x2\x2\x2\x153\x19\x3\x2\x2\x2\x154\x152\x3\x2\x2\x2\x155\x156\a\x44\x2"+
		"\x2\x156\x157\x5\x1C\xF\x2\x157\x158\a\x38\x2\x2\x158\x1B\x3\x2\x2\x2"+
		"\x159\x15E\x5,\x17\x2\x15A\x15B\a!\x2\x2\x15B\x15D\x5,\x17\x2\x15C\x15A"+
		"\x3\x2\x2\x2\x15D\x160\x3\x2\x2\x2\x15E\x15C\x3\x2\x2\x2\x15E\x15F\x3"+
		"\x2\x2\x2\x15F\x1D\x3\x2\x2\x2\x160\x15E\x3\x2\x2\x2\x161\x163\a\x15\x2"+
		"\x2\x162\x164\x5 \x11\x2\x163\x162\x3\x2\x2\x2\x163\x164\x3\x2\x2\x2\x164"+
		"\x165\x3\x2\x2\x2\x165\x166\x5\x84\x43\x2\x166\x1F\x3\x2\x2\x2\x167\x168"+
		"\a\x61\x2\x2\x168\x169\x5\"\x12\x2\x169\x16A\a\x10\x2\x2\x16A!\x3\x2\x2"+
		"\x2\x16B\x170\x5$\x13\x2\x16C\x16D\a!\x2\x2\x16D\x16F\x5$\x13\x2\x16E"+
		"\x16C\x3\x2\x2\x2\x16F\x172\x3\x2\x2\x2\x170\x16E\x3\x2\x2\x2\x170\x171"+
		"\x3\x2\x2\x2\x171#\x3\x2\x2\x2\x172\x170\x3\x2\x2\x2\x173\x17C\at\x2\x2"+
		"\x174\x175\at\x2\x2\x175\x176\aW\x2\x2\x176\x17C\at\x2\x2\x177\x178\a"+
		"t\x2\x2\x178\x179\aW\x2\x2\x179\x17A\at\x2\x2\x17A\x17C\a`\x2\x2\x17B"+
		"\x173\x3\x2\x2\x2\x17B\x174\x3\x2\x2\x2\x17B\x177\x3\x2\x2\x2\x17C%\x3"+
		"\x2\x2\x2\x17D\x17E\at\x2\x2\x17E\'\x3\x2\x2\x2\x17F\x180\at\x2\x2\x180"+
		")\x3\x2\x2\x2\x181\x182\at\x2\x2\x182+\x3\x2\x2\x2\x183\x184\at\x2\x2"+
		"\x184-\x3\x2\x2\x2\x185\x189\a\x64\x2\x2\x186\x188\x5\x84\x43\x2\x187"+
		"\x186\x3\x2\x2\x2\x188\x18B\x3\x2\x2\x2\x189\x187\x3\x2\x2\x2\x189\x18A"+
		"\x3\x2\x2\x2\x18A\x18C\x3\x2\x2\x2\x18B\x189\x3\x2\x2\x2\x18C\x1AA\a\t"+
		"\x2\x2\x18D\x18E\a\x64\x2\x2\x18E\x190\x5\x30\x19\x2\x18F\x191\x5\x84"+
		"\x43\x2\x190\x18F\x3\x2\x2\x2\x191\x192\x3\x2\x2\x2\x192\x190\x3\x2\x2"+
		"\x2\x192\x193\x3\x2\x2\x2\x193\x194\x3\x2\x2\x2\x194\x195\a\t\x2\x2\x195"+
		"\x1AA\x3\x2\x2\x2\x196\x198\a\x64\x2\x2\x197\x199\x5\x84\x43\x2\x198\x197"+
		"\x3\x2\x2\x2\x199\x19A\x3\x2\x2\x2\x19A\x198\x3\x2\x2\x2\x19A\x19B\x3"+
		"\x2\x2\x2\x19B\x19C\x3\x2\x2\x2\x19C\x19D\x5.\x18\x2\x19D\x19E\a\t\x2"+
		"\x2\x19E\x1AA\x3\x2\x2\x2\x19F\x1A0\a\x64\x2\x2\x1A0\x1A2\x5\x30\x19\x2"+
		"\x1A1\x1A3\x5\x84\x43\x2\x1A2\x1A1\x3\x2\x2\x2\x1A3\x1A4\x3\x2\x2\x2\x1A4"+
		"\x1A2\x3\x2\x2\x2\x1A4\x1A5\x3\x2\x2\x2\x1A5\x1A6\x3\x2\x2\x2\x1A6\x1A7"+
		"\x5.\x18\x2\x1A7\x1A8\a\t\x2\x2\x1A8\x1AA\x3\x2\x2\x2\x1A9\x185\x3\x2"+
		"\x2\x2\x1A9\x18D\x3\x2\x2\x2\x1A9\x196\x3\x2\x2\x2\x1A9\x19F\x3\x2\x2"+
		"\x2\x1AA/\x3\x2\x2\x2\x1AB\x1AC\t\x2\x2\x2\x1AC\x31\x3\x2\x2\x2\x1AD\x1B2"+
		"\x5x=\x2\x1AE\x1B2\x5\x34\x1B\x2\x1AF\x1B2\x5\x36\x1C\x2\x1B0\x1B2\x5"+
		"\x1E\x10\x2\x1B1\x1AD\x3\x2\x2\x2\x1B1\x1AE\x3\x2\x2\x2\x1B1\x1AF\x3\x2"+
		"\x2\x2\x1B1\x1B0\x3\x2\x2\x2\x1B2\x1B3\x3\x2\x2\x2\x1B3\x1B1\x3\x2\x2"+
		"\x2\x1B3\x1B4\x3\x2\x2\x2\x1B4\x33\x3\x2\x2\x2\x1B5\x1B6\a\x35\x2\x2\x1B6"+
		"\x1B7\x5\x38\x1D\x2\x1B7\x35\x3\x2\x2\x2\x1B8\x1B9\a$\x2\x2\x1B9\x1BA"+
		"\x5\x38\x1D\x2\x1BA\x37\x3\x2\x2\x2\x1BB\x1BD\x5H%\x2\x1BC\x1BB\x3\x2"+
		"\x2\x2\x1BC\x1BD\x3\x2\x2\x2\x1BD\x1BE\x3\x2\x2\x2\x1BE\x1BF\x5\x42\""+
		"\x2\x1BF\x1C0\an\x2\x2\x1C0\x39\x3\x2\x2\x2\x1C1\x1C7\x5v<\x2\x1C2\x1C7"+
		"\x5x=\x2\x1C3\x1C7\x5<\x1F\x2\x1C4\x1C7\x5> \x2\x1C5\x1C7\x5J&\x2\x1C6"+
		"\x1C1\x3\x2\x2\x2\x1C6\x1C2\x3\x2\x2\x2\x1C6\x1C3\x3\x2\x2\x2\x1C6\x1C4"+
		"\x3\x2\x2\x2\x1C6\x1C5\x3\x2\x2\x2\x1C7\x1C8\x3\x2\x2\x2\x1C8\x1C6\x3"+
		"\x2\x2\x2\x1C8\x1C9\x3\x2\x2\x2\x1C9;\x3\x2\x2\x2\x1CA\x1CB\a\x35\x2\x2"+
		"\x1CB\x1CC\x5@!\x2\x1CC=\x3\x2\x2\x2\x1CD\x1CE\a$\x2\x2\x1CE\x1CF\x5@"+
		"!\x2\x1CF?\x3\x2\x2\x2\x1D0\x1D2\x5H%\x2\x1D1\x1D0\x3\x2\x2\x2\x1D1\x1D2"+
		"\x3\x2\x2\x2\x1D2\x1D3\x3\x2\x2\x2\x1D3\x1D5\x5\x42\"\x2\x1D4\x1D6\x5"+
		"~@\x2\x1D5\x1D4\x3\x2\x2\x2\x1D5\x1D6\x3\x2\x2\x2\x1D6\x1D7\x3\x2\x2\x2"+
		"\x1D7\x1D8\x5\xACW\x2\x1D8\x41\x3\x2\x2\x2\x1D9\x1E3\x5\x46$\x2\x1DA\x1DC"+
		"\x5\x44#\x2\x1DB\x1DA\x3\x2\x2\x2\x1DC\x1DD\x3\x2\x2\x2\x1DD\x1DB\x3\x2"+
		"\x2\x2\x1DD\x1DE\x3\x2\x2\x2\x1DE\x1E0\x3\x2\x2\x2\x1DF\x1E1\x5\x98M\x2"+
		"\x1E0\x1DF\x3\x2\x2\x2\x1E0\x1E1\x3\x2\x2\x2\x1E1\x1E3\x3\x2\x2\x2\x1E2"+
		"\x1D9\x3\x2\x2\x2\x1E2\x1DB\x3\x2\x2\x2\x1E3\x43\x3\x2\x2\x2\x1E4\x1E6"+
		"\x5\x46$\x2\x1E5\x1E4\x3\x2\x2\x2\x1E5\x1E6\x3\x2\x2\x2\x1E6\x1E7\x3\x2"+
		"\x2\x2\x1E7\x1EB\a`\x2\x2\x1E8\x1EA\x5H%\x2\x1E9\x1E8\x3\x2\x2\x2\x1EA"+
		"\x1ED\x3\x2\x2\x2\x1EB\x1E9\x3\x2\x2\x2\x1EB\x1EC\x3\x2\x2\x2\x1EC\x1EE"+
		"\x3\x2\x2\x2\x1ED\x1EB\x3\x2\x2\x2\x1EE\x1EF\at\x2\x2\x1EF\x45\x3\x2\x2"+
		"\x2\x1F0\x1F1\at\x2\x2\x1F1G\x3\x2\x2\x2\x1F2\x1F3\a\x61\x2\x2\x1F3\x1F4"+
		"\x5\x9EP\x2\x1F4\x1F5\a\x10\x2\x2\x1F5I\x3\x2\x2\x2\x1F6\x1F7\aO\x2\x2"+
		"\x1F7\x1F8\x5L\'\x2\x1F8\x1F9\an\x2\x2\x1F9\x1FF\x3\x2\x2\x2\x1FA\x1FB"+
		"\aP\x2\x2\x1FB\x1FC\x5L\'\x2\x1FC\x1FD\an\x2\x2\x1FD\x1FF\x3\x2\x2\x2"+
		"\x1FE\x1F6\x3\x2\x2\x2\x1FE\x1FA\x3\x2\x2\x2\x1FFK\x3\x2\x2\x2\x200\x205"+
		"\x5N(\x2\x201\x202\a!\x2\x2\x202\x204\x5N(\x2\x203\x201\x3\x2\x2\x2\x204"+
		"\x207\x3\x2\x2\x2\x205\x203\x3\x2\x2\x2\x205\x206\x3\x2\x2\x2\x206M\x3"+
		"\x2\x2\x2\x207\x205\x3\x2\x2\x2\x208\x20D\at\x2\x2\x209\x20A\at\x2\x2"+
		"\x20A\x20B\aW\x2\x2\x20B\x20D\at\x2\x2\x20C\x208\x3\x2\x2\x2\x20C\x209"+
		"\x3\x2\x2\x2\x20DO\x3\x2\x2\x2\x20E\x223\a(\x2\x2\x20F\x223\a\v\x2\x2"+
		"\x210\x223\a\x1E\x2\x2\x211\x223\a&\x2\x2\x212\x223\a\x1B\x2\x2\x213\x223"+
		"\a\n\x2\x2\x214\x223\a\a\x2\x2\x215\x223\aU\x2\x2\x216\x223\a\x12\x2\x2"+
		"\x217\x219\aI\x2\x2\x218\x21A\x5\x1A\xE\x2\x219\x218\x3\x2\x2\x2\x219"+
		"\x21A\x3\x2\x2\x2\x21A\x223\x3\x2\x2\x2\x21B\x21D\x5&\x14\x2\x21C\x21E"+
		"\x5\x1A\xE\x2\x21D\x21C\x3\x2\x2\x2\x21D\x21E\x3\x2\x2\x2\x21E\x223\x3"+
		"\x2\x2\x2\x21F\x223\x5\x82\x42\x2\x220\x223\x5\x8CG\x2\x221\x223\at\x2"+
		"\x2\x222\x20E\x3\x2\x2\x2\x222\x20F\x3\x2\x2\x2\x222\x210\x3\x2\x2\x2"+
		"\x222\x211\x3\x2\x2\x2\x222\x212\x3\x2\x2\x2\x222\x213\x3\x2\x2\x2\x222"+
		"\x214\x3\x2\x2\x2\x222\x215\x3\x2\x2\x2\x222\x216\x3\x2\x2\x2\x222\x217"+
		"\x3\x2\x2\x2\x222\x21B\x3\x2\x2\x2\x222\x21F\x3\x2\x2\x2\x222\x220\x3"+
		"\x2\x2\x2\x222\x221\x3\x2\x2\x2\x223Q\x3\x2\x2\x2\x224\x228\aX\x2\x2\x225"+
		"\x228\ao\x2\x2\x226\x228\x5T+\x2\x227\x224\x3\x2\x2\x2\x227\x225\x3\x2"+
		"\x2\x2\x227\x226\x3\x2\x2\x2\x228S\x3\x2\x2\x2\x229\x22A\t\x3\x2\x2\x22A"+
		"U\x3\x2\x2\x2\x22B\x238\at\x2\x2\x22C\x238\x5\xDEp\x2\x22D\x238\av\x2"+
		"\x2\x22E\x22F\a\x61\x2\x2\x22F\x230\x5\xB4[\x2\x230\x231\a\x10\x2\x2\x231"+
		"\x238\x3\x2\x2\x2\x232\x238\a\x3\x2\x2\x233\x238\x5X-\x2\x234\x238\x5"+
		"`\x31\x2\x235\x238\x5\x64\x33\x2\x236\x238\x5\x66\x34\x2\x237\x22B\x3"+
		"\x2\x2\x2\x237\x22C\x3\x2\x2\x2\x237\x22D\x3\x2\x2\x2\x237\x22E\x3\x2"+
		"\x2\x2\x237\x232\x3\x2\x2\x2\x237\x233\x3\x2\x2\x2\x237\x234\x3\x2\x2"+
		"\x2\x237\x235\x3\x2\x2\x2\x237\x236\x3\x2\x2\x2\x238W\x3\x2\x2\x2\x239"+
		"\x23A\a\x41\x2\x2\x23A\x23B\x5Z.\x2\x23B\x23C\x5\\/\x2\x23C\x23D\a]\x2"+
		"\x2\x23DY\x3\x2\x2\x2\x23E\x242\x5\xB4[\x2\x23F\x242\x5&\x14\x2\x240\x242"+
		"\aQ\x2\x2\x241\x23E\x3\x2\x2\x2\x241\x23F\x3\x2\x2\x2\x241\x240\x3\x2"+
		"\x2\x2\x242[\x3\x2\x2\x2\x243\x24A\x5\x46$\x2\x244\x246\x5^\x30\x2\x245"+
		"\x244\x3\x2\x2\x2\x246\x247\x3\x2\x2\x2\x247\x245\x3\x2\x2\x2\x247\x248"+
		"\x3\x2\x2\x2\x248\x24A\x3\x2\x2\x2\x249\x243\x3\x2\x2\x2\x249\x245\x3"+
		"\x2\x2\x2\x24A]\x3\x2\x2\x2\x24B\x24D\x5\x46$\x2\x24C\x24B\x3\x2\x2\x2"+
		"\x24C\x24D\x3\x2\x2\x2\x24D\x24E\x3\x2\x2\x2\x24E\x24F\a`\x2\x2\x24F\x250"+
		"\x5\xB4[\x2\x250_\x3\x2\x2\x2\x251\x252\aK\x2\x2\x252\x253\a\x61\x2\x2"+
		"\x253\x254\x5\x62\x32\x2\x254\x255\a\x10\x2\x2\x255\x61\x3\x2\x2\x2\x256"+
		"\x260\x5\x46$\x2\x257\x259\x5\x46$\x2\x258\x257\x3\x2\x2\x2\x258\x259"+
		"\x3\x2\x2\x2\x259\x25A\x3\x2\x2\x2\x25A\x25C\a`\x2\x2\x25B\x258\x3\x2"+
		"\x2\x2\x25C\x25D\x3\x2\x2\x2\x25D\x25B\x3\x2\x2\x2\x25D\x25E\x3\x2\x2"+
		"\x2\x25E\x260\x3\x2\x2\x2\x25F\x256\x3\x2\x2\x2\x25F\x25B\x3\x2\x2\x2"+
		"\x260\x63\x3\x2\x2\x2\x261\x262\aL\x2\x2\x262\x263\a\x61\x2\x2\x263\x264"+
		"\x5,\x17\x2\x264\x265\a\x10\x2\x2\x265\x65\x3\x2\x2\x2\x266\x267\a\x18"+
		"\x2\x2\x267\x268\a\x61\x2\x2\x268\x269\x5\x9EP\x2\x269\x26A\a\x10\x2\x2"+
		"\x26Ag\x3\x2\x2\x2\x26B\x26C\x5\x92J\x2\x26Ci\x3\x2\x2\x2\x26D\x26E\a"+
		"\x11\x2\x2\x26Ek\x3\x2\x2\x2\x26F\x270\aV\x2\x2\x270\x271\a\x61\x2\x2"+
		"\x271\x272\x5h\x35\x2\x272\x273\a\x10\x2\x2\x273\x274\x5\xA8U\x2\x274"+
		"m\x3\x2\x2\x2\x275\x276\a#\x2\x2\x276\x277\x5\xA8U\x2\x277o\x3\x2\x2\x2"+
		"\x278\x279\a?\x2\x2\x279\x27A\a\x61\x2\x2\x27A\x27B\at\x2\x2\x27B\x27C"+
		"\a\x10\x2\x2\x27Cq\x3\x2\x2\x2\x27D\x27E\x5j\x36\x2\x27E\x280\x5l\x37"+
		"\x2\x27F\x281\x5n\x38\x2\x280\x27F\x3\x2\x2\x2\x280\x281\x3\x2\x2\x2\x281"+
		"s\x3\x2\x2\x2\x282\x283\a\x6\x2\x2\x283\x284\a\x61\x2\x2\x284\x285\at"+
		"\x2\x2\x285\x286\a\x10\x2\x2\x286\x287\x5\xA8U\x2\x287u\x3\x2\x2\x2\x288"+
		"\x28A\x5z>\x2\x289\x288\x3\x2\x2\x2\x289\x28A\x3\x2\x2\x2\x28A\x28B\x3"+
		"\x2\x2\x2\x28B\x28C\x5\x92J\x2\x28C\x28D\x5\xACW\x2\x28Dw\x3\x2\x2\x2"+
		"\x28E\x290\x5z>\x2\x28F\x291\x5~@\x2\x290\x28F\x3\x2\x2\x2\x290\x291\x3"+
		"\x2\x2\x2\x291\x292\x3\x2\x2\x2\x292\x293\an\x2\x2\x293y\x3\x2\x2\x2\x294"+
		"\x298\x5|?\x2\x295\x298\x5P)\x2\x296\x298\x5R*\x2\x297\x294\x3\x2\x2\x2"+
		"\x297\x295\x3\x2\x2\x2\x297\x296\x3\x2\x2\x2\x298\x299\x3\x2\x2\x2\x299"+
		"\x297\x3\x2\x2\x2\x299\x29A\x3\x2\x2\x2\x29A{\x3\x2\x2\x2\x29B\x29C\t"+
		"\x4\x2\x2\x29C}\x3\x2\x2\x2\x29D\x2A2\x5\x80\x41\x2\x29E\x29F\a!\x2\x2"+
		"\x29F\x2A1\x5\x80\x41\x2\x2A0\x29E\x3\x2\x2\x2\x2A1\x2A4\x3\x2\x2\x2\x2A2"+
		"\x2A0\x3\x2\x2\x2\x2A2\x2A3\x3\x2\x2\x2\x2A3\x7F\x3\x2\x2\x2\x2A4\x2A2"+
		"\x3\x2\x2\x2\x2A5\x2A8\x5\x92J\x2\x2A6\x2A7\aW\x2\x2\x2A7\x2A9\x5\x9C"+
		"O\x2\x2A8\x2A6\x3\x2\x2\x2\x2A8\x2A9\x3\x2\x2\x2\x2A9\x81\x3\x2\x2\x2"+
		"\x2AA\x2B7\t\x5\x2\x2\x2AB\x2B8\at\x2\x2\x2AC\x2AE\at\x2\x2\x2AD\x2AC"+
		"\x3\x2\x2\x2\x2AD\x2AE\x3\x2\x2\x2\x2AE\x2AF\x3\x2\x2\x2\x2AF\x2B1\a\x64"+
		"\x2\x2\x2B0\x2B2\x5\x84\x43\x2\x2B1\x2B0\x3\x2\x2\x2\x2B2\x2B3\x3\x2\x2"+
		"\x2\x2B3\x2B1\x3\x2\x2\x2\x2B3\x2B4\x3\x2\x2\x2\x2B4\x2B5\x3\x2\x2\x2"+
		"\x2B5\x2B6\a\t\x2\x2\x2B6\x2B8\x3\x2\x2\x2\x2B7\x2AB\x3\x2\x2\x2\x2B7"+
		"\x2AD\x3\x2\x2\x2\x2B8\x83\x3\x2\x2\x2\x2B9\x2BA\x5\x86\x44\x2\x2BA\x2BB"+
		"\x5\x88\x45\x2\x2BB\x2BC\an\x2\x2\x2BC\x85\x3\x2\x2\x2\x2BD\x2C0\x5P)"+
		"\x2\x2BE\x2C0\x5R*\x2\x2BF\x2BD\x3\x2\x2\x2\x2BF\x2BE\x3\x2\x2\x2\x2C0"+
		"\x2C1\x3\x2\x2\x2\x2C1\x2BF\x3\x2\x2\x2\x2C1\x2C2\x3\x2\x2\x2\x2C2\x87"+
		"\x3\x2\x2\x2\x2C3\x2C8\x5\x8A\x46\x2\x2C4\x2C5\a!\x2\x2\x2C5\x2C7\x5\x8A"+
		"\x46\x2\x2C6\x2C4\x3\x2\x2\x2\x2C7\x2CA\x3\x2\x2\x2\x2C8\x2C6\x3\x2\x2"+
		"\x2\x2C8\x2C9\x3\x2\x2\x2\x2C9\x89\x3\x2\x2\x2\x2CA\x2C8\x3\x2\x2\x2\x2CB"+
		"\x2D2\x5\x92J\x2\x2CC\x2CE\x5\x92J\x2\x2CD\x2CC\x3\x2\x2\x2\x2CD\x2CE"+
		"\x3\x2\x2\x2\x2CE\x2CF\x3\x2\x2\x2\x2CF\x2D0\a`\x2\x2\x2D0\x2D2\x5\xDE"+
		"p\x2\x2D1\x2CB\x3\x2\x2\x2\x2D1\x2CD\x3\x2\x2\x2\x2D2\x8B\x3\x2\x2\x2"+
		"\x2D3\x2DF\aZ\x2\x2\x2D4\x2D9\x5\xDCo\x2\x2D5\x2D6\a\x64\x2\x2\x2D6\x2D7"+
		"\x5\x8EH\x2\x2D7\x2D8\a\t\x2\x2\x2D8\x2DA\x3\x2\x2\x2\x2D9\x2D5\x3\x2"+
		"\x2\x2\x2D9\x2DA\x3\x2\x2\x2\x2DA\x2E0\x3\x2\x2\x2\x2DB\x2DC\a\x64\x2"+
		"\x2\x2DC\x2DD\x5\x8EH\x2\x2DD\x2DE\a\t\x2\x2\x2DE\x2E0\x3\x2\x2\x2\x2DF"+
		"\x2D4\x3\x2\x2\x2\x2DF\x2DB\x3\x2\x2\x2\x2E0\x8D\x3\x2\x2\x2\x2E1\x2E6"+
		"\x5\x90I\x2\x2E2\x2E3\a!\x2\x2\x2E3\x2E5\x5\x90I\x2\x2E4\x2E2\x3\x2\x2"+
		"\x2\x2E5\x2E8\x3\x2\x2\x2\x2E6\x2E4\x3\x2\x2\x2\x2E6\x2E7\x3\x2\x2\x2"+
		"\x2E7\x8F\x3\x2\x2\x2\x2E8\x2E6\x3\x2\x2\x2\x2E9\x2EC\x5\xDCo\x2\x2EA"+
		"\x2EB\aW\x2\x2\x2EB\x2ED\x5\xBC_\x2\x2EC\x2EA\x3\x2\x2\x2\x2EC\x2ED\x3"+
		"\x2\x2\x2\x2ED\x91\x3\x2\x2\x2\x2EE\x2F2\a\x5\x2\x2\x2EF\x2F1\x5R*\x2"+
		"\x2F0\x2EF\x3\x2\x2\x2\x2F1\x2F4\x3\x2\x2\x2\x2F2\x2F0\x3\x2\x2\x2\x2F2"+
		"\x2F3\x3\x2\x2\x2\x2F3\x2F5\x3\x2\x2\x2\x2F4\x2F2\x3\x2\x2\x2\x2F5\x2F8"+
		"\x5\x92J\x2\x2F6\x2F8\x5\x94K\x2\x2F7\x2EE\x3\x2\x2\x2\x2F7\x2F6\x3\x2"+
		"\x2\x2\x2F8\x93\x3\x2\x2\x2\x2F9\x2FD\x5\xDCo\x2\x2FA\x2FC\x5\x96L\x2"+
		"\x2FB\x2FA\x3\x2\x2\x2\x2FC\x2FF\x3\x2\x2\x2\x2FD\x2FB\x3\x2\x2\x2\x2FD"+
		"\x2FE\x3\x2\x2\x2\x2FE\x30A\x3\x2\x2\x2\x2FF\x2FD\x3\x2\x2\x2\x300\x301"+
		"\a\x61\x2\x2\x301\x302\x5\x92J\x2\x302\x306\a\x10\x2\x2\x303\x305\x5\x96"+
		"L\x2\x304\x303\x3\x2\x2\x2\x305\x308\x3\x2\x2\x2\x306\x304\x3\x2\x2\x2"+
		"\x306\x307\x3\x2\x2\x2\x307\x30A\x3\x2\x2\x2\x308\x306\x3\x2\x2\x2\x309"+
		"\x2F9\x3\x2\x2\x2\x309\x300\x3\x2\x2\x2\x30A\x95\x3\x2\x2\x2\x30B\x30D"+
		"\a\x41\x2\x2\x30C\x30E\x5\xBC_\x2\x30D\x30C\x3\x2\x2\x2\x30D\x30E\x3\x2"+
		"\x2\x2\x30E\x30F\x3\x2\x2\x2\x30F\x316\a]\x2\x2\x310\x312\a\x61\x2\x2"+
		"\x311\x313\x5\x98M\x2\x312\x311\x3\x2\x2\x2\x312\x313\x3\x2\x2\x2\x313"+
		"\x314\x3\x2\x2\x2\x314\x316\a\x10\x2\x2\x315\x30B\x3\x2\x2\x2\x315\x310"+
		"\x3\x2\x2\x2\x316\x97\x3\x2\x2\x2\x317\x31A\x5\xA4S\x2\x318\x319\a!\x2"+
		"\x2\x319\x31B\a+\x2\x2\x31A\x318\x3\x2\x2\x2\x31A\x31B\x3\x2\x2\x2\x31B"+
		"\x99\x3\x2\x2\x2\x31C\x321\x5z>\x2\x31D\x31F\x5\x92J\x2\x31E\x31D\x3\x2"+
		"\x2\x2\x31E\x31F\x3\x2\x2\x2\x31F\x322\x3\x2\x2\x2\x320\x322\x5\xA0Q\x2"+
		"\x321\x31E\x3\x2\x2\x2\x321\x320\x3\x2\x2\x2\x322\x9B\x3\x2\x2\x2\x323"+
		"\x330\x5\xB6\\\x2\x324\x325\a\x64\x2\x2\x325\x32A\x5\x9CO\x2\x326\x327"+
		"\a!\x2\x2\x327\x329\x5\x9CO\x2\x328\x326\x3\x2\x2\x2\x329\x32C\x3\x2\x2"+
		"\x2\x32A\x328\x3\x2\x2\x2\x32A\x32B\x3\x2\x2\x2\x32B\x32D\x3\x2\x2\x2"+
		"\x32C\x32A\x3\x2\x2\x2\x32D\x32E\a\t\x2\x2\x32E\x330\x3\x2\x2\x2\x32F"+
		"\x323\x3\x2\x2\x2\x32F\x324\x3\x2\x2\x2\x330\x9D\x3\x2\x2\x2\x331\x332"+
		"\x5\x86\x44\x2\x332\x333\x5\xA0Q\x2\x333\x9F\x3\x2\x2\x2\x334\x338\a\x5"+
		"\x2\x2\x335\x337\x5R*\x2\x336\x335\x3\x2\x2\x2\x337\x33A\x3\x2\x2\x2\x338"+
		"\x336\x3\x2\x2\x2\x338\x339\x3\x2\x2\x2\x339\x33B\x3\x2\x2\x2\x33A\x338"+
		"\x3\x2\x2\x2\x33B\x34F\x5\xA0Q\x2\x33C\x33D\a\x61\x2\x2\x33D\x33E\x5\xA0"+
		"Q\x2\x33E\x340\a\x10\x2\x2\x33F\x341\x5\xA2R\x2\x340\x33F\x3\x2\x2\x2"+
		"\x341\x342\x3\x2\x2\x2\x342\x340\x3\x2\x2\x2\x342\x343\x3\x2\x2\x2\x343"+
		"\x34F\x3\x2\x2\x2\x344\x346\a\x41\x2\x2\x345\x347\x5\xBC_\x2\x346\x345"+
		"\x3\x2\x2\x2\x346\x347\x3\x2\x2\x2\x347\x348\x3\x2\x2\x2\x348\x34A\a]"+
		"\x2\x2\x349\x344\x3\x2\x2\x2\x34A\x34B\x3\x2\x2\x2\x34B\x349\x3\x2\x2"+
		"\x2\x34B\x34C\x3\x2\x2\x2\x34C\x34F\x3\x2\x2\x2\x34D\x34F\x3\x2\x2\x2"+
		"\x34E\x334\x3\x2\x2\x2\x34E\x33C\x3\x2\x2\x2\x34E\x349\x3\x2\x2\x2\x34E"+
		"\x34D\x3\x2\x2\x2\x34F\xA1\x3\x2\x2\x2\x350\x352\a\x41\x2\x2\x351\x353"+
		"\x5\xBC_\x2\x352\x351\x3\x2\x2\x2\x352\x353\x3\x2\x2\x2\x353\x354\x3\x2"+
		"\x2\x2\x354\x35B\a]\x2\x2\x355\x357\a\x61\x2\x2\x356\x358\x5\xA4S\x2\x357"+
		"\x356\x3\x2\x2\x2\x357\x358\x3\x2\x2\x2\x358\x359\x3\x2\x2\x2\x359\x35B"+
		"\a\x10\x2\x2\x35A\x350\x3\x2\x2\x2\x35A\x355\x3\x2\x2\x2\x35B\xA3\x3\x2"+
		"\x2\x2\x35C\x361\x5\x9AN\x2\x35D\x35E\a!\x2\x2\x35E\x360\x5\x9AN\x2\x35F"+
		"\x35D\x3\x2\x2\x2\x360\x363\x3\x2\x2\x2\x361\x35F\x3\x2\x2\x2\x361\x362"+
		"\x3\x2\x2\x2\x362\xA5\x3\x2\x2\x2\x363\x361\x3\x2\x2\x2\x364\x366\x5\xA8"+
		"U\x2\x365\x364\x3\x2\x2\x2\x366\x367\x3\x2\x2\x2\x367\x365\x3\x2\x2\x2"+
		"\x367\x368\x3\x2\x2\x2\x368\xA7\x3\x2\x2\x2\x369\x373\x5\xAAV\x2\x36A"+
		"\x36B\x5\xB4[\x2\x36B\x36C\an\x2\x2\x36C\x373\x3\x2\x2\x2\x36D\x373\x5"+
		"\xACW\x2\x36E\x373\x5\xAEX\x2\x36F\x373\x5\xB0Y\x2\x370\x373\x5\xB2Z\x2"+
		"\x371\x373\an\x2\x2\x372\x369\x3\x2\x2\x2\x372\x36A\x3\x2\x2\x2\x372\x36D"+
		"\x3\x2\x2\x2\x372\x36E\x3\x2\x2\x2\x372\x36F\x3\x2\x2\x2\x372\x370\x3"+
		"\x2\x2\x2\x372\x371\x3\x2\x2\x2\x373\xA9\x3\x2\x2\x2\x374\x375\x5\xDC"+
		"o\x2\x375\x376\a`\x2\x2\x376\x377\x5\xA8U\x2\x377\x381\x3\x2\x2\x2\x378"+
		"\x379\aM\x2\x2\x379\x37A\x5\xBC_\x2\x37A\x37B\a`\x2\x2\x37B\x37C\x5\xA8"+
		"U\x2\x37C\x381\x3\x2\x2\x2\x37D\x37E\a^\x2\x2\x37E\x37F\a`\x2\x2\x37F"+
		"\x381\x5\xA8U\x2\x380\x374\x3\x2\x2\x2\x380\x378\x3\x2\x2\x2\x380\x37D"+
		"\x3\x2\x2\x2\x381\xAB\x3\x2\x2\x2\x382\x387\a\x64\x2\x2\x383\x386\x5x"+
		"=\x2\x384\x386\x5\xA6T\x2\x385\x383\x3\x2\x2\x2\x385\x384\x3\x2\x2\x2"+
		"\x386\x389\x3\x2\x2\x2\x387\x385\x3\x2\x2\x2\x387\x388\x3\x2\x2\x2\x388"+
		"\x38A\x3\x2\x2\x2\x389\x387\x3\x2\x2\x2\x38A\x38B\a\t\x2\x2\x38B\xAD\x3"+
		"\x2\x2\x2\x38C\x38D\a%\x2\x2\x38D\x38E\a\x61\x2\x2\x38E\x38F\x5\xB4[\x2"+
		"\x38F\x390\a\x10\x2\x2\x390\x393\x5\xA8U\x2\x391\x392\a/\x2\x2\x392\x394"+
		"\x5\xA8U\x2\x393\x391\x3\x2\x2\x2\x393\x394\x3\x2\x2\x2\x394\x39C\x3\x2"+
		"\x2\x2\x395\x396\a;\x2\x2\x396\x397\a\x61\x2\x2\x397\x398\x5\xB4[\x2\x398"+
		"\x399\a\x10\x2\x2\x399\x39A\x5\xA8U\x2\x39A\x39C\x3\x2\x2\x2\x39B\x38C"+
		"\x3\x2\x2\x2\x39B\x395\x3\x2\x2\x2\x39C\xAF\x3\x2\x2\x2\x39D\x39E\a\""+
		"\x2\x2\x39E\x39F\a\x61\x2\x2\x39F\x3A0\x5\xB4[\x2\x3A0\x3A1\a\x10\x2\x2"+
		"\x3A1\x3A2\x5\xA8U\x2\x3A2\x3BB\x3\x2\x2\x2\x3A3\x3A4\a\f\x2\x2\x3A4\x3A5"+
		"\x5\xA8U\x2\x3A5\x3A6\a\"\x2\x2\x3A6\x3A7\a\x61\x2\x2\x3A7\x3A8\x5\xB4"+
		"[\x2\x3A8\x3A9\a\x10\x2\x2\x3A9\x3AA\an\x2\x2\x3AA\x3BB\x3\x2\x2\x2\x3AB"+
		"\x3AC\ak\x2\x2\x3AC\x3AE\a\x61\x2\x2\x3AD\x3AF\x5\xB4[\x2\x3AE\x3AD\x3"+
		"\x2\x2\x2\x3AE\x3AF\x3\x2\x2\x2\x3AF\x3B0\x3\x2\x2\x2\x3B0\x3B2\an\x2"+
		"\x2\x3B1\x3B3\x5\xB4[\x2\x3B2\x3B1\x3\x2\x2\x2\x3B2\x3B3\x3\x2\x2\x2\x3B3"+
		"\x3B4\x3\x2\x2\x2\x3B4\x3B6\an\x2\x2\x3B5\x3B7\x5\xB4[\x2\x3B6\x3B5\x3"+
		"\x2\x2\x2\x3B6\x3B7\x3\x2\x2\x2\x3B7\x3B8\x3\x2\x2\x2\x3B8\x3B9\a\x10"+
		"\x2\x2\x3B9\x3BB\x5\xA8U\x2\x3BA\x39D\x3\x2\x2\x2\x3BA\x3A3\x3\x2\x2\x2"+
		"\x3BA\x3AB\x3\x2\x2\x2\x3BB\xB1\x3\x2\x2\x2\x3BC\x3BD\a\x14\x2\x2\x3BD"+
		"\x3BE\x5\xDCo\x2\x3BE\x3BF\an\x2\x2\x3BF\x3CA\x3\x2\x2\x2\x3C0\x3C1\a"+
		"\x46\x2\x2\x3C1\x3CA\an\x2\x2\x3C2\x3C3\a,\x2\x2\x3C3\x3CA\an\x2\x2\x3C4"+
		"\x3C6\al\x2\x2\x3C5\x3C7\x5\xB4[\x2\x3C6\x3C5\x3\x2\x2\x2\x3C6\x3C7\x3"+
		"\x2\x2\x2\x3C7\x3C8\x3\x2\x2\x2\x3C8\x3CA\an\x2\x2\x3C9\x3BC\x3\x2\x2"+
		"\x2\x3C9\x3C0\x3\x2\x2\x2\x3C9\x3C2\x3\x2\x2\x2\x3C9\x3C4\x3\x2\x2\x2"+
		"\x3CA\xB3\x3\x2\x2\x2\x3CB\x3D0\x5\xB6\\\x2\x3CC\x3CD\a!\x2\x2\x3CD\x3CF"+
		"\x5\xB6\\\x2\x3CE\x3CC\x3\x2\x2\x2\x3CF\x3D2\x3\x2\x2\x2\x3D0\x3CE\x3"+
		"\x2\x2\x2\x3D0\x3D1\x3\x2\x2\x2\x3D1\xB5\x3\x2\x2\x2\x3D2\x3D0\x3\x2\x2"+
		"\x2\x3D3\x3D7\x5\xBA^\x2\x3D4\x3D5\x5\xB8]\x2\x3D5\x3D6\x5\xB6\\\x2\x3D6"+
		"\x3D8\x3\x2\x2\x2\x3D7\x3D4\x3\x2\x2\x2\x3D7\x3D8\x3\x2\x2\x2\x3D8\xB7"+
		"\x3\x2\x2\x2\x3D9\x3DA\t\x6\x2\x2\x3DA\xB9\x3\x2\x2\x2\x3DB\x3E1\x5\xBE"+
		"`\x2\x3DC\x3DD\a\'\x2\x2\x3DD\x3DE\x5\xBE`\x2\x3DE\x3DF\a`\x2\x2\x3DF"+
		"\x3E0\x5\xBE`\x2\x3E0\x3E2\x3\x2\x2\x2\x3E1\x3DC\x3\x2\x2\x2\x3E1\x3E2"+
		"\x3\x2\x2\x2\x3E2\xBB\x3\x2\x2\x2\x3E3\x3E4\x5\xBA^\x2\x3E4\xBD\x3\x2"+
		"\x2\x2\x3E5\x3EA\x5\xC0\x61\x2\x3E6\x3E7\a\x37\x2\x2\x3E7\x3E9\x5\xC0"+
		"\x61\x2\x3E8\x3E6\x3\x2\x2\x2\x3E9\x3EC\x3\x2\x2\x2\x3EA\x3E8\x3\x2\x2"+
		"\x2\x3EA\x3EB\x3\x2\x2\x2\x3EB\xBF\x3\x2\x2\x2\x3EC\x3EA\x3\x2\x2\x2\x3ED"+
		"\x3F2\x5\xC2\x62\x2\x3EE\x3EF\a\x36\x2\x2\x3EF\x3F1\x5\xC2\x62\x2\x3F0"+
		"\x3EE\x3\x2\x2\x2\x3F1\x3F4\x3\x2\x2\x2\x3F2\x3F0\x3\x2\x2\x2\x3F2\x3F3"+
		"\x3\x2\x2\x2\x3F3\xC1\x3\x2\x2\x2\x3F4\x3F2\x3\x2\x2\x2\x3F5\x3FA\x5\xC4"+
		"\x63\x2\x3F6\x3F7\a\x19\x2\x2\x3F7\x3F9\x5\xC4\x63\x2\x3F8\x3F6\x3\x2"+
		"\x2\x2\x3F9\x3FC\x3\x2\x2\x2\x3FA\x3F8\x3\x2\x2\x2\x3FA\x3FB\x3\x2\x2"+
		"\x2\x3FB\xC3\x3\x2\x2\x2\x3FC\x3FA\x3\x2\x2\x2\x3FD\x402\x5\xC6\x64\x2"+
		"\x3FE\x3FF\ai\x2\x2\x3FF\x401\x5\xC6\x64\x2\x400\x3FE\x3\x2\x2\x2\x401"+
		"\x404\x3\x2\x2\x2\x402\x400\x3\x2\x2\x2\x402\x403\x3\x2\x2\x2\x403\xC5"+
		"\x3\x2\x2\x2\x404\x402\x3\x2\x2\x2\x405\x40A\x5\xC8\x65\x2\x406\x407\a"+
		"@\x2\x2\x407\x409\x5\xC8\x65\x2\x408\x406\x3\x2\x2\x2\x409\x40C\x3\x2"+
		"\x2\x2\x40A\x408\x3\x2\x2\x2\x40A\x40B\x3\x2\x2\x2\x40B\xC7\x3\x2\x2\x2"+
		"\x40C\x40A\x3\x2\x2\x2\x40D\x412\x5\xCA\x66\x2\x40E\x40F\t\a\x2\x2\x40F"+
		"\x411\x5\xCA\x66\x2\x410\x40E\x3\x2\x2\x2\x411\x414\x3\x2\x2\x2\x412\x410"+
		"\x3\x2\x2\x2\x412\x413\x3\x2\x2\x2\x413\xC9\x3\x2\x2\x2\x414\x412\x3\x2"+
		"\x2\x2\x415\x41A\x5\xCCg\x2\x416\x417\t\b\x2\x2\x417\x419\x5\xCCg\x2\x418"+
		"\x416\x3\x2\x2\x2\x419\x41C\x3\x2\x2\x2\x41A\x418\x3\x2\x2\x2\x41A\x41B"+
		"\x3\x2\x2\x2\x41B\xCB\x3\x2\x2\x2\x41C\x41A\x3\x2\x2\x2\x41D\x422\x5\xCE"+
		"h\x2\x41E\x41F\t\t\x2\x2\x41F\x421\x5\xCEh\x2\x420\x41E\x3\x2\x2\x2\x421"+
		"\x424\x3\x2\x2\x2\x422\x420\x3\x2\x2\x2\x422\x423\x3\x2\x2\x2\x423\xCD"+
		"\x3\x2\x2\x2\x424\x422\x3\x2\x2\x2\x425\x42A\x5\xD0i\x2\x426\x427\t\n"+
		"\x2\x2\x427\x429\x5\xD0i\x2\x428\x426\x3\x2\x2\x2\x429\x42C\x3\x2\x2\x2"+
		"\x42A\x428\x3\x2\x2\x2\x42A\x42B\x3\x2\x2\x2\x42B\xCF\x3\x2\x2\x2\x42C"+
		"\x42A\x3\x2\x2\x2\x42D\x432\x5\xD2j\x2\x42E\x42F\t\v\x2\x2\x42F\x431\x5"+
		"\xD2j\x2\x430\x42E\x3\x2\x2\x2\x431\x434\x3\x2\x2\x2\x432\x430\x3\x2\x2"+
		"\x2\x432\x433\x3\x2\x2\x2\x433\xD1\x3\x2\x2\x2\x434\x432\x3\x2\x2\x2\x435"+
		"\x436\a\x61\x2\x2\x436\x437\x5\x9EP\x2\x437\x438\a\x10\x2\x2\x438\x439"+
		"\x5\xD2j\x2\x439\x43C\x3\x2\x2\x2\x43A\x43C\x5\xD4k\x2\x43B\x435\x3\x2"+
		"\x2\x2\x43B\x43A\x3\x2\x2\x2\x43C\xD3\x3\x2\x2\x2\x43D\x44E\x5\xD8m\x2"+
		"\x43E\x43F\a\x32\x2\x2\x43F\x44E\x5\xD4k\x2\x440\x441\a\x45\x2\x2\x441"+
		"\x44E\x5\xD4k\x2\x442\x443\x5\xD6l\x2\x443\x444\x5\xD2j\x2\x444\x44E\x3"+
		"\x2\x2\x2\x445\x44B\a\x1C\x2\x2\x446\x447\a\x61\x2\x2\x447\x448\x5\x9E"+
		"P\x2\x448\x449\a\x10\x2\x2\x449\x44C\x3\x2\x2\x2\x44A\x44C\x5\xD4k\x2"+
		"\x44B\x446\x3\x2\x2\x2\x44B\x44A\x3\x2\x2\x2\x44C\x44E\x3\x2\x2\x2\x44D"+
		"\x43D\x3\x2\x2\x2\x44D\x43E\x3\x2\x2\x2\x44D\x440\x3\x2\x2\x2\x44D\x442"+
		"\x3\x2\x2\x2\x44D\x445\x3\x2\x2\x2\x44E\xD5\x3\x2\x2\x2\x44F\x450\t\f"+
		"\x2\x2\x450\xD7\x3\x2\x2\x2\x451\x463\x5V,\x2\x452\x453\a\x41\x2\x2\x453"+
		"\x454\x5\xB4[\x2\x454\x455\a]\x2\x2\x455\x462\x3\x2\x2\x2\x456\x458\a"+
		"\x61\x2\x2\x457\x459\x5\xDAn\x2\x458\x457\x3\x2\x2\x2\x458\x459\x3\x2"+
		"\x2\x2\x459\x45A\x3\x2\x2\x2\x45A\x462\a\x10\x2\x2\x45B\x45C\a\x34\x2"+
		"\x2\x45C\x462\x5\xDCo\x2\x45D\x45E\aS\x2\x2\x45E\x462\x5\xDCo\x2\x45F"+
		"\x462\a\x32\x2\x2\x460\x462\a\x45\x2\x2\x461\x452\x3\x2\x2\x2\x461\x456"+
		"\x3\x2\x2\x2\x461\x45B\x3\x2\x2\x2\x461\x45D\x3\x2\x2\x2\x461\x45F\x3"+
		"\x2\x2\x2\x461\x460\x3\x2\x2\x2\x462\x465\x3\x2\x2\x2\x463\x461\x3\x2"+
		"\x2\x2\x463\x464\x3\x2\x2\x2\x464\xD9\x3\x2\x2\x2\x465\x463\x3\x2\x2\x2"+
		"\x466\x46B\x5\xB6\\\x2\x467\x468\a!\x2\x2\x468\x46A\x5\xB6\\\x2\x469\x467"+
		"\x3\x2\x2\x2\x46A\x46D\x3\x2\x2\x2\x46B\x469\x3\x2\x2\x2\x46B\x46C\x3"+
		"\x2\x2\x2\x46C\xDB\x3\x2\x2\x2\x46D\x46B\x3\x2\x2\x2\x46E\x46F\at\x2\x2"+
		"\x46F\xDD\x3\x2\x2\x2\x470\x471\t\r\x2\x2\x471\xDF\x3\x2\x2\x2\x80\xE3"+
		"\xF3\x102\x10A\x10D\x110\x113\x11B\x11F\x122\x12A\x12D\x137\x13E\x141"+
		"\x152\x15E\x163\x170\x17B\x189\x192\x19A\x1A4\x1A9\x1B1\x1B3\x1BC\x1C6"+
		"\x1C8\x1D1\x1D5\x1DD\x1E0\x1E2\x1E5\x1EB\x1FE\x205\x20C\x219\x21D\x222"+
		"\x227\x237\x241\x247\x249\x24C\x258\x25D\x25F\x280\x289\x290\x297\x299"+
		"\x2A2\x2A8\x2AD\x2B3\x2B7\x2BF\x2C1\x2C8\x2CD\x2D1\x2D9\x2DF\x2E6\x2EC"+
		"\x2F2\x2F7\x2FD\x306\x309\x30D\x312\x315\x31A\x31E\x321\x32A\x32F\x338"+
		"\x342\x346\x34B\x34E\x352\x357\x35A\x361\x367\x372\x380\x385\x387\x393"+
		"\x39B\x3AE\x3B2\x3B6\x3BA\x3C6\x3C9\x3D0\x3D7\x3E1\x3EA\x3F2\x3FA\x402"+
		"\x40A\x412\x41A\x422\x42A\x432\x43B\x44B\x44D\x458\x461\x463\x46B";
	public static readonly ATN _ATN =
		ATNSimulator.Deserialize(_serializedATN.ToCharArray());
}
} // namespace TestObjC

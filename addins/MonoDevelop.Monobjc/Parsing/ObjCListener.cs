// Generated from ObjC.g4 by ANTLR 4.0.1-SNAPSHOT
namespace MonobjcDevelop.Monobjc.Parsing {
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

public interface IObjCListener : IParseTreeListener {
	void EnterSynchronized_statement(ObjCParser.Synchronized_statementContext context);
	void ExitSynchronized_statement(ObjCParser.Synchronized_statementContext context);

	void EnterTry_statement(ObjCParser.Try_statementContext context);
	void ExitTry_statement(ObjCParser.Try_statementContext context);

	void EnterDeclarator(ObjCParser.DeclaratorContext context);
	void ExitDeclarator(ObjCParser.DeclaratorContext context);

	void EnterReceiver(ObjCParser.ReceiverContext context);
	void ExitReceiver(ObjCParser.ReceiverContext context);

	void EnterProperty_implementation(ObjCParser.Property_implementationContext context);
	void ExitProperty_implementation(ObjCParser.Property_implementationContext context);

	void EnterException_declarator(ObjCParser.Exception_declaratorContext context);
	void ExitException_declarator(ObjCParser.Exception_declaratorContext context);

	void EnterLabeled_statement(ObjCParser.Labeled_statementContext context);
	void ExitLabeled_statement(ObjCParser.Labeled_statementContext context);

	void EnterMessage_expression(ObjCParser.Message_expressionContext context);
	void ExitMessage_expression(ObjCParser.Message_expressionContext context);

	void EnterCompound_statement(ObjCParser.Compound_statementContext context);
	void ExitCompound_statement(ObjCParser.Compound_statementContext context);

	void EnterCast_expression(ObjCParser.Cast_expressionContext context);
	void ExitCast_expression(ObjCParser.Cast_expressionContext context);

	void EnterEquality_expression(ObjCParser.Equality_expressionContext context);
	void ExitEquality_expression(ObjCParser.Equality_expressionContext context);

	void EnterStorage_class_specifier(ObjCParser.Storage_class_specifierContext context);
	void ExitStorage_class_specifier(ObjCParser.Storage_class_specifierContext context);

	void EnterClass_interface(ObjCParser.Class_interfaceContext context);
	void ExitClass_interface(ObjCParser.Class_interfaceContext context);

	void EnterFunction_definition(ObjCParser.Function_definitionContext context);
	void ExitFunction_definition(ObjCParser.Function_definitionContext context);

	void EnterTranslation_unit(ObjCParser.Translation_unitContext context);
	void ExitTranslation_unit(ObjCParser.Translation_unitContext context);

	void EnterType_qualifier(ObjCParser.Type_qualifierContext context);
	void ExitType_qualifier(ObjCParser.Type_qualifierContext context);

	void EnterProtocol_list(ObjCParser.Protocol_listContext context);
	void ExitProtocol_list(ObjCParser.Protocol_listContext context);

	void EnterLogical_and_expression(ObjCParser.Logical_and_expressionContext context);
	void ExitLogical_and_expression(ObjCParser.Logical_and_expressionContext context);

	void EnterShift_expression(ObjCParser.Shift_expressionContext context);
	void ExitShift_expression(ObjCParser.Shift_expressionContext context);

	void EnterMethod_declaration(ObjCParser.Method_declarationContext context);
	void ExitMethod_declaration(ObjCParser.Method_declarationContext context);

	void EnterType_name(ObjCParser.Type_nameContext context);
	void ExitType_name(ObjCParser.Type_nameContext context);

	void EnterMethod_definition(ObjCParser.Method_definitionContext context);
	void ExitMethod_definition(ObjCParser.Method_definitionContext context);

	void EnterParameter_list(ObjCParser.Parameter_listContext context);
	void ExitParameter_list(ObjCParser.Parameter_listContext context);

	void EnterEncode_expression(ObjCParser.Encode_expressionContext context);
	void ExitEncode_expression(ObjCParser.Encode_expressionContext context);

	void EnterInclusive_or_expression(ObjCParser.Inclusive_or_expressionContext context);
	void ExitInclusive_or_expression(ObjCParser.Inclusive_or_expressionContext context);

	void EnterConstant_expression(ObjCParser.Constant_expressionContext context);
	void ExitConstant_expression(ObjCParser.Constant_expressionContext context);

	void EnterRelational_expression(ObjCParser.Relational_expressionContext context);
	void ExitRelational_expression(ObjCParser.Relational_expressionContext context);

	void EnterClass_name(ObjCParser.Class_nameContext context);
	void ExitClass_name(ObjCParser.Class_nameContext context);

	void EnterDeclaration_specifiers(ObjCParser.Declaration_specifiersContext context);
	void ExitDeclaration_specifiers(ObjCParser.Declaration_specifiersContext context);

	void EnterParameter_declaration_list(ObjCParser.Parameter_declaration_listContext context);
	void ExitParameter_declaration_list(ObjCParser.Parameter_declaration_listContext context);

	void EnterPostfix_expression(ObjCParser.Postfix_expressionContext context);
	void ExitPostfix_expression(ObjCParser.Postfix_expressionContext context);

	void EnterMessage_selector(ObjCParser.Message_selectorContext context);
	void ExitMessage_selector(ObjCParser.Message_selectorContext context);

	void EnterKeyword_declarator(ObjCParser.Keyword_declaratorContext context);
	void ExitKeyword_declarator(ObjCParser.Keyword_declaratorContext context);

	void EnterProperty_declaration(ObjCParser.Property_declarationContext context);
	void ExitProperty_declaration(ObjCParser.Property_declarationContext context);

	void EnterPreprocessor_declaration(ObjCParser.Preprocessor_declarationContext context);
	void ExitPreprocessor_declaration(ObjCParser.Preprocessor_declarationContext context);

	void EnterProperty_attributes_declaration(ObjCParser.Property_attributes_declarationContext context);
	void ExitProperty_attributes_declaration(ObjCParser.Property_attributes_declarationContext context);

	void EnterSelection_statement(ObjCParser.Selection_statementContext context);
	void ExitSelection_statement(ObjCParser.Selection_statementContext context);

	void EnterProtocol_reference_list(ObjCParser.Protocol_reference_listContext context);
	void ExitProtocol_reference_list(ObjCParser.Protocol_reference_listContext context);

	void EnterFinally_statement(ObjCParser.Finally_statementContext context);
	void ExitFinally_statement(ObjCParser.Finally_statementContext context);

	void EnterTry_block(ObjCParser.Try_blockContext context);
	void ExitTry_block(ObjCParser.Try_blockContext context);

	void EnterClass_method_definition(ObjCParser.Class_method_definitionContext context);
	void ExitClass_method_definition(ObjCParser.Class_method_definitionContext context);

	void EnterProtocol_declaration_list(ObjCParser.Protocol_declaration_listContext context);
	void ExitProtocol_declaration_list(ObjCParser.Protocol_declaration_listContext context);

	void EnterProperty_synthesize_list(ObjCParser.Property_synthesize_listContext context);
	void ExitProperty_synthesize_list(ObjCParser.Property_synthesize_listContext context);

	void EnterClass_declaration_list(ObjCParser.Class_declaration_listContext context);
	void ExitClass_declaration_list(ObjCParser.Class_declaration_listContext context);

	void EnterConditional_expression(ObjCParser.Conditional_expressionContext context);
	void ExitConditional_expression(ObjCParser.Conditional_expressionContext context);

	void EnterPrimary_expression(ObjCParser.Primary_expressionContext context);
	void ExitPrimary_expression(ObjCParser.Primary_expressionContext context);

	void EnterKeyword_argument(ObjCParser.Keyword_argumentContext context);
	void ExitKeyword_argument(ObjCParser.Keyword_argumentContext context);

	void EnterSpecifier_qualifier_list(ObjCParser.Specifier_qualifier_listContext context);
	void ExitSpecifier_qualifier_list(ObjCParser.Specifier_qualifier_listContext context);

	void EnterVisibility_specification(ObjCParser.Visibility_specificationContext context);
	void ExitVisibility_specification(ObjCParser.Visibility_specificationContext context);

	void EnterCategory_interface(ObjCParser.Category_interfaceContext context);
	void ExitCategory_interface(ObjCParser.Category_interfaceContext context);

	void EnterInitializer(ObjCParser.InitializerContext context);
	void ExitInitializer(ObjCParser.InitializerContext context);

	void EnterSelector(ObjCParser.SelectorContext context);
	void ExitSelector(ObjCParser.SelectorContext context);

	void EnterProtocol_declaration(ObjCParser.Protocol_declarationContext context);
	void ExitProtocol_declaration(ObjCParser.Protocol_declarationContext context);

	void EnterExpression(ObjCParser.ExpressionContext context);
	void ExitExpression(ObjCParser.ExpressionContext context);

	void EnterProperty_attributes_list(ObjCParser.Property_attributes_listContext context);
	void ExitProperty_attributes_list(ObjCParser.Property_attributes_listContext context);

	void EnterAssignment_expression(ObjCParser.Assignment_expressionContext context);
	void ExitAssignment_expression(ObjCParser.Assignment_expressionContext context);

	void EnterMultiplicative_expression(ObjCParser.Multiplicative_expressionContext context);
	void ExitMultiplicative_expression(ObjCParser.Multiplicative_expressionContext context);

	void EnterProperty_attribute(ObjCParser.Property_attributeContext context);
	void ExitProperty_attribute(ObjCParser.Property_attributeContext context);

	void EnterJump_statement(ObjCParser.Jump_statementContext context);
	void ExitJump_statement(ObjCParser.Jump_statementContext context);

	void EnterInstance_method_definition(ObjCParser.Instance_method_definitionContext context);
	void ExitInstance_method_definition(ObjCParser.Instance_method_definitionContext context);

	void EnterEnumerator(ObjCParser.EnumeratorContext context);
	void ExitEnumerator(ObjCParser.EnumeratorContext context);

	void EnterProtocol_qualifier(ObjCParser.Protocol_qualifierContext context);
	void ExitProtocol_qualifier(ObjCParser.Protocol_qualifierContext context);

	void EnterStruct_declarator_list(ObjCParser.Struct_declarator_listContext context);
	void ExitStruct_declarator_list(ObjCParser.Struct_declarator_listContext context);

	void EnterInterface_declaration_list(ObjCParser.Interface_declaration_listContext context);
	void ExitInterface_declaration_list(ObjCParser.Interface_declaration_listContext context);

	void EnterStruct_declarator(ObjCParser.Struct_declaratorContext context);
	void ExitStruct_declarator(ObjCParser.Struct_declaratorContext context);

	void EnterProtocol_expression(ObjCParser.Protocol_expressionContext context);
	void ExitProtocol_expression(ObjCParser.Protocol_expressionContext context);

	void EnterClass_method_declaration(ObjCParser.Class_method_declarationContext context);
	void ExitClass_method_declaration(ObjCParser.Class_method_declarationContext context);

	void EnterSelector_name(ObjCParser.Selector_nameContext context);
	void ExitSelector_name(ObjCParser.Selector_nameContext context);

	void EnterDeclaration(ObjCParser.DeclarationContext context);
	void ExitDeclaration(ObjCParser.DeclarationContext context);

	void EnterInit_declarator_list(ObjCParser.Init_declarator_listContext context);
	void ExitInit_declarator_list(ObjCParser.Init_declarator_listContext context);

	void EnterInit_declarator(ObjCParser.Init_declaratorContext context);
	void ExitInit_declarator(ObjCParser.Init_declaratorContext context);

	void EnterStruct_or_union_specifier(ObjCParser.Struct_or_union_specifierContext context);
	void ExitStruct_or_union_specifier(ObjCParser.Struct_or_union_specifierContext context);

	void EnterExclusive_or_expression(ObjCParser.Exclusive_or_expressionContext context);
	void ExitExclusive_or_expression(ObjCParser.Exclusive_or_expressionContext context);

	void EnterEnumerator_list(ObjCParser.Enumerator_listContext context);
	void ExitEnumerator_list(ObjCParser.Enumerator_listContext context);

	void EnterStatement(ObjCParser.StatementContext context);
	void ExitStatement(ObjCParser.StatementContext context);

	void EnterAdditive_expression(ObjCParser.Additive_expressionContext context);
	void ExitAdditive_expression(ObjCParser.Additive_expressionContext context);

	void EnterUnary_operator(ObjCParser.Unary_operatorContext context);
	void ExitUnary_operator(ObjCParser.Unary_operatorContext context);

	void EnterCategory_name(ObjCParser.Category_nameContext context);
	void ExitCategory_name(ObjCParser.Category_nameContext context);

	void EnterInstance_variables(ObjCParser.Instance_variablesContext context);
	void ExitInstance_variables(ObjCParser.Instance_variablesContext context);

	void EnterIteration_statement(ObjCParser.Iteration_statementContext context);
	void ExitIteration_statement(ObjCParser.Iteration_statementContext context);

	void EnterLogical_or_expression(ObjCParser.Logical_or_expressionContext context);
	void ExitLogical_or_expression(ObjCParser.Logical_or_expressionContext context);

	void EnterThrow_statement(ObjCParser.Throw_statementContext context);
	void ExitThrow_statement(ObjCParser.Throw_statementContext context);

	void EnterIdentifier(ObjCParser.IdentifierContext context);
	void ExitIdentifier(ObjCParser.IdentifierContext context);

	void EnterArgument_expression_list(ObjCParser.Argument_expression_listContext context);
	void ExitArgument_expression_list(ObjCParser.Argument_expression_listContext context);

	void EnterClass_implementation(ObjCParser.Class_implementationContext context);
	void ExitClass_implementation(ObjCParser.Class_implementationContext context);

	void EnterStruct_declaration(ObjCParser.Struct_declarationContext context);
	void ExitStruct_declaration(ObjCParser.Struct_declarationContext context);

	void EnterEnum_specifier(ObjCParser.Enum_specifierContext context);
	void ExitEnum_specifier(ObjCParser.Enum_specifierContext context);

	void EnterParameter_declaration(ObjCParser.Parameter_declarationContext context);
	void ExitParameter_declaration(ObjCParser.Parameter_declarationContext context);

	void EnterMethod_selector(ObjCParser.Method_selectorContext context);
	void ExitMethod_selector(ObjCParser.Method_selectorContext context);

	void EnterAssignment_operator(ObjCParser.Assignment_operatorContext context);
	void ExitAssignment_operator(ObjCParser.Assignment_operatorContext context);

	void EnterUnary_expression(ObjCParser.Unary_expressionContext context);
	void ExitUnary_expression(ObjCParser.Unary_expressionContext context);

	void EnterInstance_method_declaration(ObjCParser.Instance_method_declarationContext context);
	void ExitInstance_method_declaration(ObjCParser.Instance_method_declarationContext context);

	void EnterMethod_type(ObjCParser.Method_typeContext context);
	void ExitMethod_type(ObjCParser.Method_typeContext context);

	void EnterProtocol_name(ObjCParser.Protocol_nameContext context);
	void ExitProtocol_name(ObjCParser.Protocol_nameContext context);

	void EnterCategory_implementation(ObjCParser.Category_implementationContext context);
	void ExitCategory_implementation(ObjCParser.Category_implementationContext context);

	void EnterConstant(ObjCParser.ConstantContext context);
	void ExitConstant(ObjCParser.ConstantContext context);

	void EnterStatement_list(ObjCParser.Statement_listContext context);
	void ExitStatement_list(ObjCParser.Statement_listContext context);

	void EnterAbstract_declarator(ObjCParser.Abstract_declaratorContext context);
	void ExitAbstract_declarator(ObjCParser.Abstract_declaratorContext context);

	void EnterExternal_declaration(ObjCParser.External_declarationContext context);
	void ExitExternal_declaration(ObjCParser.External_declarationContext context);

	void EnterAbstract_declarator_suffix(ObjCParser.Abstract_declarator_suffixContext context);
	void ExitAbstract_declarator_suffix(ObjCParser.Abstract_declarator_suffixContext context);

	void EnterClass_list(ObjCParser.Class_listContext context);
	void ExitClass_list(ObjCParser.Class_listContext context);

	void EnterProperty_synthesize_item(ObjCParser.Property_synthesize_itemContext context);
	void ExitProperty_synthesize_item(ObjCParser.Property_synthesize_itemContext context);

	void EnterDirect_declarator(ObjCParser.Direct_declaratorContext context);
	void ExitDirect_declarator(ObjCParser.Direct_declaratorContext context);

	void EnterImplementation_definition_list(ObjCParser.Implementation_definition_listContext context);
	void ExitImplementation_definition_list(ObjCParser.Implementation_definition_listContext context);

	void EnterAnd_expression(ObjCParser.And_expressionContext context);
	void ExitAnd_expression(ObjCParser.And_expressionContext context);

	void EnterMacro_specification(ObjCParser.Macro_specificationContext context);
	void ExitMacro_specification(ObjCParser.Macro_specificationContext context);

	void EnterSuperclass_name(ObjCParser.Superclass_nameContext context);
	void ExitSuperclass_name(ObjCParser.Superclass_nameContext context);

	void EnterCatch_statement(ObjCParser.Catch_statementContext context);
	void ExitCatch_statement(ObjCParser.Catch_statementContext context);

	void EnterType_specifier(ObjCParser.Type_specifierContext context);
	void ExitType_specifier(ObjCParser.Type_specifierContext context);

	void EnterSelector_expression(ObjCParser.Selector_expressionContext context);
	void ExitSelector_expression(ObjCParser.Selector_expressionContext context);

	void EnterDeclarator_suffix(ObjCParser.Declarator_suffixContext context);
	void ExitDeclarator_suffix(ObjCParser.Declarator_suffixContext context);
}
} // namespace TestObjC

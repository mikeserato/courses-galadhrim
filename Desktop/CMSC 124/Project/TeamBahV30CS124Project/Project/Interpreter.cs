/*
 *  TEAM BAH V3.0
 *  LOLCode Interpreter
 *  December 4, 2014
 *  
 *  This class is for the actual interpretation of lexemes scanned in the lexical analyzer.
 *  It scans for lexemes and matches it the right operation to give the program context.
 *  
 */

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public class Interpreter
    {
        Form1 main_form;
        String[] lexemes;
        Stack stack;

        static Regex NUMBR = new Regex(@"^(-)?[0-9]+$", RegexOptions.None);
        static Regex NUMBAR = new Regex(@"^(-)?[0-9]+\.[0-9]+$", RegexOptions.None);
        static Regex TROOF = new Regex(@"(^WIN$|^FAIL$)", RegexOptions.None);
        static Regex YARN = new Regex(@"^""[\w\W]*""$", RegexOptions.None);
        static Regex VAR_NAME = new Regex(@"^[A-Za-z][A-Za-z0-9_]*$", RegexOptions.None);

        static Regex ARITHMETIC_OPERATION = new Regex(@"(^SUM\s+OF$|^DIFF\s+OF$|^PRODUKT\s+OF$|^QUOSHUNT\s+OF$|^MOD\s+OF$|^BIGGR\s+OF$|^SMALLR\s+OF$)", RegexOptions.None);
        static Regex COMPARISON_OPERATION_1 = new Regex(@"(^BOTH\s+SAEM$|^DIFFRINT$)", RegexOptions.None);
        static Regex COMPARISON_OPERATION_2 = new Regex(@"(^AND\s+BIGGR\s+OF$|^AND\s+SMALLR\s+OF$)", RegexOptions.None);
        
        static Regex TYPE_CASTING = new Regex(@"(^MAEK$|^IS\s+NOW\s+A$|^R\s+MAEK$)", RegexOptions.None);

        static Regex VAR_DECLARATION = new Regex(@"^I\s+HAS\s+A$", RegexOptions.None);

        static Regex CONDITIONALS = new Regex(@"(^O\s+RLY\?$|^YA\s+RLY$|^NO\s+WAI$|^MEBBE$)", RegexOptions.None);

        static Regex BOOLEAN_OPERATION = new Regex(@"(^BOTH\s+OF$|^EITHER\s+OF$|^WON\s+OF$)", RegexOptions.None);
        static Regex BOOLEAN_OPERATION_2 = new Regex(@"^NOT$");

        static Regex EXPRESSION = new Regex(@"(" + ARITHMETIC_OPERATION + "|" + COMPARISON_OPERATION_1 + "|" + BOOLEAN_OPERATION + "|" + BOOLEAN_OPERATION_2 + "|^SMOOSH$)", RegexOptions.None);

        struct type_value 
        {
            public String type;
            public String value;
        }

        struct switch_index
        {
            public int index;
            public int gtfo_index;
        }

        private Dictionary<string, type_value> dictionary;

        // class constructor 1
        public Interpreter()
        { }

        // class constructor 2
        public Interpreter(Form1 form1, string[] lexemes)
        {
            this.main_form = form1;
            this.lexemes = lexemes;
            this.stack = new Stack();
            this.dictionary = new Dictionary<string, type_value>();
            dictionary.Add("IT", new type_value { });
        }

        // this method starts the actual interpreation of the text
        public void startInterpretation()
        {
            String variable_name = "";

            for (int i = 0; i < lexemes.Length; i++)
            {
                // program end delimeter
                if (lexemes[i].CompareTo("KTHXBYE") == 0) break;

                // program start delimeter
                else if (lexemes[i].CompareTo("HAI") == 0) continue;

                // variable declaration
                else if (VAR_DECLARATION.Match(lexemes[i]).Success)
                {
                    variable_name = lexemes[++i];

                    int j = i + 1;

                    Boolean vardec_error = false;

                    if (lexemes[j].CompareTo("ITZ") == 0)
                    {
                        i = j;
                        String variable_value = "";

                        if(EXPRESSION.Match(lexemes[++i]).Success)
                        {
                            if (ARITHMETIC_OPERATION.Match(lexemes[i]).Success)
                            { 
                                i = getLexemes(i, true, false, false, false);

                                Boolean arithmetic_operation_error = isArithmeticOperation();

                                if (arithmetic_operation_error) break;
                                else variable_value = checkAndGetValueFromDictionary("IT");
                            }

                            else if (COMPARISON_OPERATION_1.Match(lexemes[i]).Success)
                            {
                                i = getLexemes(i, false, true, false, false);

                                Boolean comparison_operation_error = isComparisonOperation();

                                if (comparison_operation_error) break;
                                else variable_value = checkAndGetValueFromDictionary("IT");
                            }

                            else if (BOOLEAN_OPERATION.Match(lexemes[i]).Success)
                            {
                                i = getLexemes(i, false, false, true, false);

                                Boolean boolean_operation_error = isBooleanOperation();

                                if (boolean_operation_error) break;
                                else variable_value = checkAndGetValueFromDictionary("IT");
                            }

                            else if (BOOLEAN_OPERATION_2.Match(lexemes[i]).Success)
                            {
                                i = getLexemes(i, false, false, false, true);

                                Boolean boolean_operation_error = isBooleanOperation2();

                                if (boolean_operation_error) break;
                                else variable_value = checkAndGetValueFromDictionary("IT");
                            }

                            else if (lexemes[i].CompareTo("SMOOSH") == 0)
                            {
                                String concatenation_expression_1, concatenation_expression_2;

                                concatenation_expression_1 = lexemes[++i];
                                if (lexemes[++i].CompareTo("AN") == 0) concatenation_expression_2 = lexemes[++i];
                                else concatenation_expression_2 = lexemes[i];

                                Boolean concatenation_error = isConcatenation(concatenation_expression_1, concatenation_expression_2);

                                if (concatenation_error) break;
                                else
                                {
                                    variable_value = checkAndGetValueFromDictionary("IT");
                                    ++i;
                                }
                            }
                        }

                        else variable_value = lexemes[i];

                        vardec_error = isVariableDeclaration(variable_name, variable_value, true);
                    }

                    else vardec_error = isVariableDeclaration(variable_name, null, false);


                    if (vardec_error)
                    {
                        this.main_form.promptError(2, variable_name, 0);
                        break;
                    }
                }

                // basic input operation
                else if (lexemes[i].CompareTo("GIMMEH") == 0)
                {
                    variable_name = lexemes[++i];
                    getTextForBasicInput(variable_name);
                }

                // basic output operation
                else if (lexemes[i].CompareTo("VISIBLE") == 0)
                {
                    int j = ++i;
                    Boolean basic_ouput_error = false;

                    // checks if the variable to be printed to the console is an expression
                    if (EXPRESSION.Match(lexemes[j]).Success)
                    {
                        // if arithmetic operation
                        if (ARITHMETIC_OPERATION.Match(lexemes[j]).Success)
                        {
                            j = getLexemes(j, true, false, false, false);

                            Boolean arithmetic_operation_error = isArithmeticOperation();

                            if (arithmetic_operation_error ) break;
                            else
                            {
                                variable_name = dictionary["IT"].value;
                                basic_ouput_error = isBasicOutput(variable_name);
                            }
                        }

                        // if comparison operation
                        else if (COMPARISON_OPERATION_1.Match(lexemes[j]).Success)
                        {
                            j = getLexemes(j, false, true, false, false);

                            Boolean comparison_operation_error = isComparisonOperation();

                            if (comparison_operation_error) break;
                            else
                            {
                                variable_name = checkAndGetValueFromDictionary("IT");
                                basic_ouput_error = isBasicOutput(variable_name);
                            }
                        }

                        // if boolean expression
                        else if (BOOLEAN_OPERATION.Match(lexemes[j]).Success)
                        {
                            j = getLexemes(j, false, false, true, false);

                            Boolean booelean_operation_error = isBooleanOperation();

                            if (booelean_operation_error) break;
                            else
                            {
                                variable_name = checkAndGetValueFromDictionary("IT");
                                basic_ouput_error = isBasicOutput(variable_name);
                            }
                        }

                        // if boolean expression
                        else if (BOOLEAN_OPERATION_2.Match(lexemes[j]).Success)
                        {
                            j = getLexemes(j, false, false, false, true);
                            Boolean booelean_operation_error = isBooleanOperation2();

                            if (booelean_operation_error) break;
                            else
                            {
                                variable_name = checkAndGetValueFromDictionary("IT");
                                basic_ouput_error = isBasicOutput(variable_name);
                            }
                        }

                        // if concatenation operation
                        else if(lexemes[j].CompareTo("SMOOSH") == 0)
                        {
                            String concatenation_expression_1, concatenation_expression_2;

                            concatenation_expression_1 = lexemes[++j];
                            if (lexemes[++j].CompareTo("AN") == 0) concatenation_expression_2 = lexemes[++j];
                            else concatenation_expression_2 = lexemes[j];

                            Boolean concatenation_error = isConcatenation(concatenation_expression_1, concatenation_expression_2);

                            if (concatenation_error) break;
                            else
                            {
                                variable_name = checkAndGetValueFromDictionary("IT");
                                basic_ouput_error = isBasicOutput(variable_name);
                                ++j;
                            }
                        }
                    }

                    // checks if variable
                    else if (VAR_NAME.Match(lexemes[j]).Success)
                    {
                        variable_name = lexemes[j];
                        basic_ouput_error = isBasicOutput(variable_name);
                    }

                    // checks if primitive data type
                    else
                    {
                        variable_name = lexemes[j];
                        basic_ouput_error = isBasicOutput(variable_name);
                    }

                    i = j;

                    if (basic_ouput_error) break;
                }

                // arithmetic operation
                else if (ARITHMETIC_OPERATION.Match(lexemes[i]).Success)
                {
                    int j = i;
                    j = getLexemes(j, true, false, false, false);
                    i = j;

                    Boolean arithmetic_operation_error = false;
                    arithmetic_operation_error = isArithmeticOperation();

                    if (arithmetic_operation_error)
                    {
                        arithmetic_operation_error = false;
                        break;
                    }
                }

                // comparison operation
                else if (COMPARISON_OPERATION_1.Match(lexemes[i]).Success)
                {
                    int j = i;
                    j = getLexemes(j, false, true, false, false);
                    i = j;

                    Boolean comparison_operation_error = false;
                    comparison_operation_error = isComparisonOperation();

                    if (comparison_operation_error) break;
                }

                // boolean operation
                else if (BOOLEAN_OPERATION.Match(lexemes[i]).Success)
                {
                    int j = i;
                    j = getLexemes(j, false, false, true, false);
                    i = j;

                    Boolean boolean_operation_error = false;
                    boolean_operation_error = isBooleanOperation();

                    if (boolean_operation_error) break;
                }

                // boolean operation
                else if (BOOLEAN_OPERATION_2.Match(lexemes[i]).Success)
                {
                    int j = i;
                    j = getLexemes(j, false, false, false, true);
                    i = j;

                    Boolean boolean_operation_error = false;
                    boolean_operation_error = isBooleanOperation2();

                    if (boolean_operation_error) break;
                }

                // type casting operation
                else if (TYPE_CASTING.Match(lexemes[i]).Success)
                {
                    String variable_expression = lexemes[++i];

                    String new_variable_type = "";

                    if (lexemes[++i].CompareTo("A") == 0) new_variable_type = lexemes[++i];
                    else new_variable_type = lexemes[i];

                    String variable_value = checkAndGetValueFromDictionary(variable_expression);

                    Boolean typecasting_error = false;

                    if (variable_value.CompareTo("NOOB") == 0 || variable_value.CompareTo("NULL") == 0) typecasting_error = isTypecasting(variable_expression, null, null, new_variable_type, true);
                    else
                    {
                        String old_variable_type = checkType(variable_value);
                        typecasting_error = isTypecasting(variable_expression, variable_value, old_variable_type, new_variable_type, false);
                    }

                    if (typecasting_error) typecasting_error = false;
                }

                // concatenation operation
                else if (lexemes[i].CompareTo("SMOOSH") == 0)
                {
                    int j = i;
                    String concatenation_expression_1, concatenation_expression_2;

                    concatenation_expression_1 = lexemes[++j];
                    if (lexemes[++j].CompareTo("AN") == 0) concatenation_expression_2 = lexemes[++j];
                    else concatenation_expression_2 = lexemes[j];

                    Boolean concatenation_error = isConcatenation(concatenation_expression_1, concatenation_expression_2);

                    if (concatenation_error) break;
                    else i = ++j;
                }

                // if-then statment
                else if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[i], @"^O\s+RLY\?$", RegexOptions.None))
                {
                    String condition = checkAndGetValueFromDictionary("IT");
                    Boolean if_then_condition = false;

                    if (!String.IsNullOrEmpty(condition) && condition.CompareTo("WIN") == 0) if_then_condition = true;
                    else if_then_condition = false;

                    int ya_rly_index = getIndexIfThen(i, true, false, false);
                    int no_wai_index = getIndexIfThen(i, false, true, false);
                    int oic_index = getIndexIfThen(i, false, false, true);
                    int[] mebbe_indices = getMebbeIndices(i, oic_index);

                    Boolean if_then_error = false;

                    if (ya_rly_index == -1 || oic_index == -1)
                    {
                        if (ya_rly_index == -1) this.main_form.promptError(9, null, 0);
                        if (oic_index == -1) this.main_form.promptError(11, null, 0);
                        break;
                    }

                    if (mebbe_indices != null && mebbe_indices.Length != 0) if_then_error = isIfThenStatementWithMebbe(if_then_condition, ya_rly_index, no_wai_index, oic_index, mebbe_indices);
                    else if_then_error = isIfThenStatement(if_then_condition, ya_rly_index, no_wai_index, oic_index);
                   
                    if (if_then_error) break;
                    else i = oic_index;
                }


                // loop
                else if(System.Text.RegularExpressions.Regex.IsMatch(lexemes[i], @"^IM\s+IN\s+YR$", RegexOptions.None))
                {
                    int k;
                    for (k = i; k < lexemes.Length; k++)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[k], @"^IM\s+OUTTA\s+YR$", RegexOptions.None)) break;
                    }

                    if (k == lexemes.Length)
                    {
                        this.main_form.promptError(20, null, 0);
                        break;
                    }

                    else
                    {
                        int im_outta_index = k;
                        int j = i + 2;

                        Boolean isUppin = false, isNerfin = false;

                        if (lexemes[j].CompareTo("UPPIN") == 0) isUppin = true;
                        else if (lexemes[j].CompareTo("NERFIN") == 0) isNerfin = true;

                        j = j + 2;

                        String loop_variable = lexemes[j];

                        String loop_variable_value = checkAndGetValueFromDictionary(loop_variable);
                        if (loop_variable_value.CompareTo("NOOB") == 0 || loop_variable_value.CompareTo("NULL") == 0) addToDictionary(loop_variable, "NUMBR", "0");

                        j = j + 1;

                        Boolean isTil = false, isWile = false;

                        if (lexemes[j].CompareTo("TIL") == 0) isTil = true;
                        else if (lexemes[j].CompareTo("WILE") == 0) isWile = true;

                        j = j + 1;

                        Boolean loop_error = isLoop(j, loop_variable, isUppin, isNerfin, isTil, isWile, im_outta_index);
                        if (loop_error) break;
                        else i = im_outta_index;
                    }
                }

                else if (VAR_NAME.Match(lexemes[i]).Success)
                {
                    int j = i + 1;
                    variable_name = lexemes[i];

                    // assignment operation
                    if (lexemes[j].CompareTo("R") == 0)
                    {
                        i = j;
                        String variable_expression = lexemes[++i];
                        Boolean assignment_operation_error = false;

                        if (NUMBR.Match(variable_expression).Success || NUMBAR.Match(variable_expression).Success || TROOF.Match(variable_expression).Success || YARN.Match(variable_expression).Success) assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, true, false, false);
                        else if (EXPRESSION.Match(variable_expression).Success)
                        {
                            // checks if arithmetic operation
                            if (ARITHMETIC_OPERATION.Match(variable_expression).Success)
                            {
                                i = getLexemes(i, true, false, false, false);

                                Boolean arithmetic_operation_error = false;
                                arithmetic_operation_error = isArithmeticOperation();

                                if (arithmetic_operation_error) break;
                                else
                                {
                                    variable_expression = dictionary["IT"].value;
                                    assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, false);
                                }
                            }

                            // checks if comparison operation
                            else if (COMPARISON_OPERATION_1.Match(variable_expression).Success)
                            {
                                i = getLexemes(i, false, true, false, false);

                                Boolean comparison_operation_error = isComparisonOperation();

                                if (comparison_operation_error) break;
                                else
                                {
                                    variable_expression = checkAndGetValueFromDictionary("IT");
                                    assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, false);
                                }
                            }

                            // checks if boolean operation
                            else if (BOOLEAN_OPERATION.Match(variable_expression).Success)
                            {
                                j = i;
                                j = getLexemes(j, false, false, true, false);

                                Boolean booelean_operation_error = isBooleanOperation();

                                if (booelean_operation_error) break;
                                else
                                {
                                    variable_expression = checkAndGetValueFromDictionary("IT");
                                    assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, true);
                                }

                                i = j;
                            }

                            // checks if boolean opearation
                            else if (BOOLEAN_OPERATION_2.Match(variable_expression).Success)
                            {
                                j = i;
                                j = getLexemes(j, false, false, false, true);
                                Boolean booelean_operation_error = isBooleanOperation2();

                                if (booelean_operation_error) break;
                                else
                                {
                                    variable_expression = checkAndGetValueFromDictionary("IT");
                                    assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, true);
                                }

                                i = j;
                            }

                            // checks if concatenation expression
                            else if (variable_expression.CompareTo("SMOOSH") == 0)
                            {
                                String concatenation_expression_1, concatenation_expression_2;

                                concatenation_expression_1 = lexemes[++i];
                                if (lexemes[++i].CompareTo("AN") == 0) concatenation_expression_2 = lexemes[++i];
                                else concatenation_expression_2 = lexemes[i];

                                Boolean concatenation_error = isConcatenation(concatenation_expression_1, concatenation_expression_2);

                                if (concatenation_error) break;
                                else
                                {
                                    variable_expression = checkAndGetValueFromDictionary("IT");
                                    assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, false);
                                }

                                ++i;
                            }
                        }
                        else if (VAR_NAME.Match(variable_expression).Success) assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, false, true);

                        if (assignment_operation_error) break;
                    }
                    
                    // recasting
                    else if (TYPE_CASTING.Match(lexemes[j]).Success)
                    {
                        Boolean recasting_error = false;

                        if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[j], @"^IS\s+NOW\s+A$", RegexOptions.None))
                        {
                            String new_variable_type = lexemes[++j];
                            String old_variable_value = checkAndGetValueFromDictionary(variable_name);

                            if (old_variable_value.CompareTo("NOOB") == 0 || old_variable_value.CompareTo("NULL") == 0) recasting_error = isTypecasting(variable_name, null, null, new_variable_type, true);
                            else
                            {
                                String old_variable_type = checkType(old_variable_value);
                                recasting_error = isTypecasting(variable_name, old_variable_value, old_variable_type, new_variable_type, false);
                            }

                            if (recasting_error) break;
                            else i = j;
                        }

                        else if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[j], @"^R\s+MAEK$", RegexOptions.None))
                        {
                            String new_variable_type = "";

                            variable_name = lexemes[++j];
                            if (lexemes[++j].CompareTo("A") == 0) new_variable_type = lexemes[++j];
                            else new_variable_type = lexemes[j];

                            String old_variable_value = checkAndGetValueFromDictionary(variable_name);

                            if (old_variable_value.CompareTo("NOOB") == 0 || old_variable_value.CompareTo("NULL") == 0) recasting_error = isTypecasting(variable_name, null, null, new_variable_type, true);
                            else
                            {
                                String old_variable_type = checkType(old_variable_value);
                                recasting_error = isTypecasting(variable_name, old_variable_value, old_variable_type, new_variable_type, false);
                            }

                            if (recasting_error) break;
                            else i = j;
                        }
                    }

                    // switch statement
                    else if (lexemes[j].CompareTo("WTF?") == 0)
                    {
                        i = j;

                        String switch_control_statement = checkAndGetValueFromDictionary("IT");
                        if (!String.IsNullOrEmpty(switch_control_statement))
                        {
                            String variable_type = checkType(switch_control_statement);

                            if (variable_type.CompareTo("YARN") == 0)
                            {
                                switch_index[] omg_gtfo_indices = getIndexSwitch(i, true, false, false);
                                switch_index[] omgwtf_index = getIndexSwitch(i, false, false, true);
                                switch_index[] oic_index = getIndexSwitch(i, false, true, false);

                                if (omg_gtfo_indices.Length == 0 || omg_gtfo_indices == null || oic_index.Length == 0 || oic_index == null)
                                {
                                    if ((omg_gtfo_indices.Length == 0 || omg_gtfo_indices == null))
                                    {
                                        this.main_form.promptError(13, null, 0);
                                        break;
                                    }

                                    else if ((oic_index.Length == 0 || oic_index == null))
                                    {
                                        this.main_form.promptError(14, null, 0);
                                        break;
                                    }
                                }

                                else
                                {
                                    Boolean switch_error = false;

                                    switch_error = isSwitchStatement(switch_control_statement, omg_gtfo_indices, (omgwtf_index == null || omgwtf_index.Length == 0) ? -1 : omgwtf_index[0].index);

                                    if (switch_error) break;
                                    else i = oic_index[0].index;
                                }

                            }

                            else
                            {
                                this.main_form.promptError(12, null, 0);
                                break;
                            }
                        }

                        else
                        {
                            this.main_form.promptError(12, null, 0);
                            break;
                        }
                    }
                }
            }
        }

        // this method performs the interpretation of variable declaration operations
        private Boolean isVariableDeclaration(String variable_name, String variable_value, Boolean is_initialized)
        {
            if (dictionary.ContainsKey(variable_name) == true) return true;
            else
            {
                if (is_initialized)
                {
                    String variable_type = checkType(variable_value);

                    if (variable_type.CompareTo("YARN") == 0)
                    {
                        if (variable_value.Contains(":)")) variable_value = variable_value.Replace(":)", Environment.NewLine);
                        if (variable_value.Contains(":>")) variable_value = variable_value.Replace(":>", "\t");
                        if (variable_value.Contains(":o")) variable_value = variable_value.Replace(":o", "(beep)");
                        if (variable_value.Contains(":\"")) variable_value = variable_value.Replace(":\"", "\"");
                        if (variable_value.Contains("::")) variable_value = variable_value.Replace("::", ":");
                    }

                    addToDictionary(variable_name, variable_type, variable_value);
                }

                else addToDictionary(variable_name, "NOOB", null);
                
                return false;
            }
        }

        // this method performs the getting of text for basic input
        public void getTextForBasicInput(String variable_name)
        {
            Form2 form = new Form2(this, variable_name);
            form.ShowDialog();
        }

        // this method gets the passed data from form 2 and adds it to the dictionary
        public void passDataForBasicInput(String variable_value, String variable_name)
        {
            String variable_type = checkType(variable_value);
            addToDictionary(variable_name, variable_type, variable_value);
        }

        // this method performs the interpretation of basic output operation
        private Boolean isBasicOutput(String variable_name)
        {
            String variable_type_1 = checkType(variable_name);
            if (variable_type_1.CompareTo("YARN") == 0)
            {
                variable_name = trimYarnDoubleQuotes(variable_name);

                if (variable_name.Contains(":)")) variable_name = variable_name.Replace(":)", Environment.NewLine);
                if (variable_name.Contains(":>")) variable_name = variable_name.Replace(":>", "\t");
                if (variable_name.Contains(":o")) variable_name = variable_name.Replace(":o", "(beep)");
                if (variable_name.Contains(":\"")) variable_name = variable_name.Replace(":\"", "\"");
                if (variable_name.Contains("::")) variable_name = variable_name.Replace("::", ":");
                
                this.main_form.printToConsole(variable_name);

                return false;
            }

            else if (variable_type_1.CompareTo("NUMBR") == 0 || variable_type_1.CompareTo("NUMBAR") == 0)
            {
                this.main_form.printToConsole(variable_name);
                return false;
            }

            else if (variable_type_1.CompareTo("TROOF") == 0)
            {
                this.main_form.printToConsole(variable_name);
                return false;
            }

            else
            {
                if (dictionary.ContainsKey(variable_name) == true)
                {
                    String variable_type_2 = dictionary[variable_name].type;
                    String variable_value = dictionary[variable_name].value;

                    if (variable_type_2.CompareTo("YARN") == 0)
                    {
                        variable_value = variable_value.TrimEnd('"');
                        variable_value = variable_value.TrimStart('"');
                        this.main_form.printToConsole(variable_value);

                        return false;
                    }

                    else if (variable_type_2.CompareTo("NOOB") == 0)
                    {
                        this.main_form.promptError(3, null, 0);
                        return true;
                    }

                    else
                    {
                        this.main_form.printToConsole(variable_value);
                        return false;
                    }
                }

                else
                {
                    this.main_form.promptError(4, variable_name, 0);
                    return true;
                }
            }
        }

        // this method gets the lexemes for an expression
        private int getLexemes(int i, Boolean isArithmeticOperation, Boolean isComparisonOperation, Boolean isBooleanOperation, Boolean isBooleanOperation2)
        {
            if (isArithmeticOperation)
            {
                int j = i;
                stack.Push(lexemes[++j]);
                j = ++j;
                stack.Push(lexemes[++j]);

                if (lexemes[i].Contains("SUM")) stack.Push("+");
                else if (lexemes[i].Contains("DIFF")) stack.Push("-");
                else if (lexemes[i].Contains("PRODUKT")) stack.Push("*");
                else if (lexemes[i].Contains("QUOSHUNT")) stack.Push("/");
                else if (lexemes[i].Contains("MOD")) stack.Push("%");
                else if (lexemes[i].Contains("BIGGR")) stack.Push(">");
                else if (lexemes[i].Contains("SMALLR")) stack.Push("<");

                i = j;

                return i;
            }

            else if (isComparisonOperation)
            {
                Boolean isBothSaem = false;
                Boolean isDiffrint = false;
                Boolean isBiggr = false;
                Boolean isSmallr = false;

                if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[i], @"^BOTH\s+SAEM$", RegexOptions.None)) isBothSaem = true;
                else if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[i], @"^DIFFRINT$", RegexOptions.None)) isDiffrint = true;

                int j = i;
                stack.Push(lexemes[++j]);
                if (COMPARISON_OPERATION_2.Match(lexemes[++j]).Success)
                {
                    stack.Pop();
                    if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[j], @"^AND\s+BIGGR\s+OF$", RegexOptions.None))
                    {
                        stack.Push(lexemes[++j]);
                        j = ++j;
                        stack.Push(lexemes[++j]);
                        isBiggr = true;
                    }

                    else
                    {
                        stack.Push(lexemes[++j]);
                        j = ++j;
                        stack.Push(lexemes[++j]);
                        isSmallr = true;                    
                    }
                }
                else if (lexemes[++j].CompareTo("AN") == 0) stack.Push(lexemes[++j]);
                else stack.Push(lexemes[j]);

                if (isBothSaem && isBiggr) stack.Push(">=");
                else if (isBothSaem && isSmallr) stack.Push("<=");
                else if (isDiffrint && isBiggr) stack.Push("<");
                else if (isDiffrint && isSmallr) stack.Push(">");
                else if (isBothSaem && !isBiggr && !isSmallr) stack.Push("==");
                else if (isDiffrint && !isBiggr && !isSmallr) stack.Push("!=");

                i = j;

                return i;
            }

            else if (isBooleanOperation)
            {
                int j = i;

                stack.Push(lexemes[++j]);
                j = ++j;

                if (lexemes[j].CompareTo("AN") == 0) stack.Push(lexemes[++j]);
                else stack.Push(lexemes[j]);
                

                if (lexemes[i].Contains("BOTH")) stack.Push("*");
                else if (lexemes[i].Contains("EITHER")) stack.Push("+");
                else if (lexemes[i].Contains("WON")) stack.Push("^");

                i = j;
                return i;
            }

            else if (isBooleanOperation2)
            {
                int j = i;

                stack.Push(lexemes[++j]);
                if (lexemes[i].Contains("NOT")) stack.Push("~");

                i = j;
                return i;

            }

            else return 1;
        }

        // this method performs the interpretation of basic arithmetic operations
        private Boolean isArithmeticOperation()
        {
            String arithmetic_operation_code = stack.Pop().ToString();

            String op_2 = stack.Pop().ToString();
            String op_1 = stack.Pop().ToString();

            String variable_type = checkType(op_2);
            if (variable_type.CompareTo("YARN") == 0) op_2 = trimYarnDoubleQuotes(op_2);
            else if (variable_type.CompareTo("TROOF") == 0)
            {
                if (op_2.CompareTo("WIN") == 0) op_2 = "1";
                else op_2 = "0";
            }
            else if (variable_type.CompareTo("NOOB") == 0)
            {
                op_2 = checkAndGetValueFromDictionary(op_2);
                variable_type = checkType(op_2);
                if (variable_type.CompareTo("YARN") == 0) op_2 = trimYarnDoubleQuotes(op_2);
                else if (variable_type.CompareTo("TROOF") == 0)
                {
                    if (op_2.CompareTo("WIN") == 0) op_2 = "1";
                    else op_2 = "0";
                }
            }
          
            variable_type = checkType(op_1);
            if (variable_type.CompareTo("YARN") == 0) op_1 = trimYarnDoubleQuotes(op_1);
            else if (variable_type.CompareTo("TROOF") == 0)
            {
                if (op_1.CompareTo("WIN") == 0) op_1 = "1";
                else op_1 = "0";
            }
            else if (variable_type.CompareTo("NOOB") == 0)
            {
                op_1 = checkAndGetValueFromDictionary(op_1);
                variable_type = checkType(op_1);
                if (variable_type.CompareTo("YARN") == 0) op_1 = trimYarnDoubleQuotes(op_1);
                else if (variable_type.CompareTo("TROOF") == 0)
                {
                    if (op_1.CompareTo("WIN") == 0) op_1 = "1";
                    else op_1 = "0";
                }
            }

            // check if the variable exists and has a value
            if (op_1.CompareTo("NULL") == 0 || op_2.CompareTo("NULL") == 0)
            {
                this.main_form.promptError(6, null, 0);
                return true;
            }

            // checks if the variable is of type NOOB
            else if (op_1.CompareTo("NOOB") == 0 || op_2.CompareTo("NOOB") == 0)
            {
                this.main_form.promptError(7, null, 0);
                return true;
            }

            else
            {
                // checks if the operand is a number or not
                Double ret_num;
                Boolean op_1_parse = double.TryParse(op_1, out ret_num);
                Boolean op_2_parse = double.TryParse(op_2, out ret_num);
                if (op_1_parse && op_2_parse)
                {
                    double operand_2 = Convert.ToDouble(op_2);
                    double operand_1 = Convert.ToDouble(op_1);
                    double answer = 0;

                    switch (arithmetic_operation_code)
                    {
                        // addition operation
                        case "+":
                            answer = operand_1 + operand_2;
                            addToDictionary("IT", null, answer.ToString());
                            break;

                        // subtraction operation
                        case "-":
                            answer = operand_1 - operand_2;
                            addToDictionary("IT", null, answer.ToString());
                            break;

                        // multiplication operation
                        case "*":
                            answer = operand_1 * operand_2;
                            addToDictionary("IT", null, answer.ToString());
                            break;

                        // division operation
                        case "/":
                            answer = operand_1 / operand_2;
                            addToDictionary("IT", null, answer.ToString());
                            break;

                        // modulo operation 
                        case "%":
                            answer = operand_1 % operand_2;
                            addToDictionary("IT", null, answer.ToString());
                            break;

                        // bigger operation
                        case ">":
                            if (operand_1 >= operand_2) answer = operand_1;
                            else answer = operand_2;
                            addToDictionary("IT", null, answer.ToString());
                            break;

                        // smaller operation
                        case "<":
                            if (operand_1 <= operand_2) answer = operand_1;
                            else answer = operand_2;
                            addToDictionary("IT", null, answer.ToString());
                            break;

                        default:
                            break;
                    }

                    return false;
                }

                // if not a number
                else
                {
                    this.main_form.promptError(5, null, 0);
                    return true;
                }
            }
        }

        // this method performs the interpretation of comparison operation
        private Boolean isComparisonOperation()
        {
            String comparison_opearation_code = stack.Pop().ToString();

            String op_2 = stack.Pop().ToString();
            String op_1 = stack.Pop().ToString();

            String variable_type = checkType(op_2);
            if (variable_type.CompareTo("YARN") == 0) op_2 = "0";
            else if (variable_type.CompareTo("TROOF") == 0)
            {
                if (op_2.CompareTo("WIN") == 0) op_2 = "1";
                else op_2 = "0";
            }
            else if (variable_type.CompareTo("NOOB") == 0)
            {
                op_2 = checkAndGetValueFromDictionary(op_2);
                variable_type = checkType(op_2);
                if (variable_type.CompareTo("YARN") == 0) op_2 = "0";
                else if (variable_type.CompareTo("TROOF") == 0)
                {
                    if (op_2.CompareTo("WIN") == 0) op_2 = "1";
                    else op_2 = "0";
                }
            }

            variable_type = checkType(op_1);
            if (variable_type.CompareTo("YARN") == 0) op_1 = "0";
            else if (variable_type.CompareTo("TROOF") == 0)
            {
                if (op_1.CompareTo("WIN") == 0) op_1 = "1";
                else op_1 = "0";
            }
            else if (variable_type.CompareTo("NOOB") == 0)
            {
                op_1 = checkAndGetValueFromDictionary(op_1);
                variable_type = checkType(op_1);
                if (variable_type.CompareTo("YARN") == 0) op_1 = "0";
                else if (variable_type.CompareTo("TROOF") == 0)
                {
                    if (op_1.CompareTo("WIN") == 0) op_1 = "1";
                    else op_1 = "0";
                }
            }

            // check if the variable exists and has a value
            if (op_1.CompareTo("NULL") == 0 || op_2.CompareTo("NULL") == 0)
            {
                this.main_form.promptError(16, null, 0);
                return true;
            }

            // checks if the variable is of type NOOB
            else if (op_1.CompareTo("NOOB") == 0 || op_2.CompareTo("NOOB") == 0)
            {
                this.main_form.promptError(17, null, 0);
                return true;
            }

            else
            {
                // checks if the operand is a number or not
                Double ret_num;
                Boolean op_1_parse = double.TryParse(op_1, out ret_num);
                Boolean op_2_parse = double.TryParse(op_2, out ret_num);
                if (op_1_parse && op_2_parse)
                {
                    double operand_2 = Convert.ToDouble(op_2);
                    double operand_1 = Convert.ToDouble(op_1);

                    switch (comparison_opearation_code)
                    {
                        
                        case "==":
                            if (operand_1 == operand_2)
                            {
                                if (operand_1 == 0 && operand_2 == 0) addToDictionary("IT", null, "FAIL");
                                else addToDictionary("IT", null, "WIN");
                            }
                            else addToDictionary("IT", null, "FAIL");
                            break;

                        
                        case "!=":
                            if(operand_1 != operand_2) addToDictionary("IT", null, "WIN");
                            else addToDictionary("IT", null, "FAIL");
                            break;

                        case ">=":
                            if (operand_1 >= operand_2) 
                            {
                                if (operand_1 == 0 && operand_2 == 0) addToDictionary("IT", null, "FAIL");
                                else addToDictionary("IT", null, "WIN");
                            }
                            else addToDictionary("IT", null, "FAIL");
                            break;

                        case "<=":
                            if (operand_1 <= operand_2)
                            {
                                if (operand_1 == 0 && operand_2 == 0) addToDictionary("IT", null, "FAIL");
                                else addToDictionary("IT", null, "WIN");
                            }
                            else addToDictionary("IT", null, "FAIL");
                            break;

                        case ">":
                            if (operand_1 > operand_2) addToDictionary("IT", null, "WIN");
                            else addToDictionary("IT", null, "FAIL");
                            break;

                        case "<":
                            if (operand_1 < operand_2) addToDictionary("IT", null, "WIN");
                            else addToDictionary("IT", null, "FAIL");
                            break;
                    }

                    return false;
                }

                // if not a number
                else
                {
                    this.main_form.promptError(15, null, 0);
                    return true;
                }
            }
        }

        // this method performs the interpretation of boolean opearation
        private Boolean isBooleanOperation()
        {

            String boolean_operation_code = stack.Pop().ToString();

            String op_2 = stack.Pop().ToString();
            String op_1 = stack.Pop().ToString();

            string variable_type = checkType(op_2);
            if (variable_type.CompareTo("NOOB") == 0)
            {
                if (dictionary.ContainsKey(op_2) == true)
                {
                    if (dictionary[op_2].type.CompareTo("TROOF") == 0) op_2 = checkAndGetValueFromDictionary(op_2);
                    else
                    {
                        this.main_form.promptError(19, null, 0);
                        return true;
                    }
                }

                else 
                {
                    this.main_form.promptError(18, null, 0);
                    return true;
                }
            }

            else if(variable_type.CompareTo("TROOF") != 0)
            {
                this.main_form.promptError(19, null, 0);
                return true;
            }

            variable_type = checkType(op_1);
            if (variable_type.CompareTo("NOOB") == 0)
            {
                if (dictionary.ContainsKey(op_1) == true)
                {
                    if (dictionary[op_1].type.CompareTo("TROOF") == 0) op_1 = checkAndGetValueFromDictionary(op_1);
                    else
                    {
                        this.main_form.promptError(19, null, 0);
                        return true;
                    }
                }

                else
                {
                    this.main_form.promptError(18, null, 0);
                    return true;
                }
            }

            else if (variable_type.CompareTo("TROOF") != 0)
            {
                this.main_form.promptError(19, null, 0);
                return true;
            }

            string answer = "";
            switch (boolean_operation_code)
            {
                // both of
                case "*":
                    if (op_1 == "WIN" && op_2 == "WIN")
                    {
                        answer = "WIN";
                        addToDictionary("IT", null, answer);
                    }
                    else
                    {
                        answer = "FAIL";
                        addToDictionary("IT", null, answer);
                    }
                    break;

                // either of
                case "+":
                    if (op_1 == "FAIL" && op_2 == "FAIL")
                    {
                        answer = "FAIL";
                        addToDictionary("IT", null, answer);
                    }
                    else
                    {
                        answer = "WIN";
                        addToDictionary("IT", null, answer);
                    }
                    break;

                // won of
                case "^":
                    if (op_1 == "WIN" && op_2 == "WIN" || op_1 == "FAIL" && op_2 == "FAIL")
                    {
                        answer = "FAIL";
                        addToDictionary("IT", null, answer);
                    }
                    else
                    {
                        answer = "WIN";
                        addToDictionary("IT", null, answer);
                    }
                    break;

                default:
                    break;
            }

            return false;
        }

        // this method performs the interpretation of boolean opearation
        private Boolean isBooleanOperation2()
        {

            String boolean_operation_code = stack.Pop().ToString();
            String op_1 = stack.Pop().ToString();

            String variable_type = checkType(op_1);
            MessageBox.Show(op_1+variable_type);
            if (variable_type.CompareTo("NOOB") == 0)
            {
                if (dictionary.ContainsKey(op_1) == true)
                {
                    if (dictionary[op_1].type.CompareTo("TROOF") == 0) op_1 = checkAndGetValueFromDictionary(op_1);
                    else
                    {
                        this.main_form.promptError(19, null, 0);
                        return true;
                    }
                }

                else
                {
                    this.main_form.promptError(18, null, 0);
                    return true;
                }
            }

            else if (variable_type.CompareTo("TROOF") != 0)
            {
                this.main_form.promptError(19, null, 0);
                return true;
            }


            string answer = "";

            switch (boolean_operation_code)
            {
                //both of
                case "~":
                    if (op_1 == "WIN")
                    {
                        answer = "FAIL";
                        addToDictionary("IT", null, answer);
                    }

                    else
                    {
                        answer = "WIN";
                        addToDictionary("IT", null, answer);
                    }
                    break;

                default:
                    break;
            }

            return false;

        }

        // this method performs the interpreation of assignment operation
        private Boolean isAssignmentOperation(String variable_name, String variable_expression, Boolean isPrimitiveDataType, Boolean isExpression, Boolean isVariable)
        { 
            // if the value to be assigned is a primitive data type
            if(isPrimitiveDataType)
            {
                String variable_type = checkType(variable_expression);
                addToDictionary(variable_name, variable_type, variable_expression);
                return false;
            }

            // if the value to be assigned is an expression
            else if(isExpression)
            {
                String variable_type = checkType(variable_expression);
                addToDictionary(variable_name, variable_type, variable_expression);
                return false;
            }

            // if the value to be assigned is a variable
            else if(isVariable)
            {
                String variable_value = checkAndGetValueFromDictionary(variable_expression);
                String variable_type = checkType(variable_value);

                if (variable_value.CompareTo("NULL") == 0)
                {
                    this.main_form.promptError(8, null, 0);
                    return true;
                }

                else if (variable_value.CompareTo("NOOB") == 0)
                {
                    variable_value = "";
                    variable_type = "NOOB";
                    addToDictionary(variable_name, variable_type, variable_value);
                    return false;
                }

                else
                {
                    addToDictionary(variable_name, variable_type, variable_value);
                    return false;
                }
            }

            else return false;
        }

        // this method performs the interpretation of typecasting operation
        private Boolean isTypecasting(String variable_expression, String old_variable_value, String old_variable_type, String new_variable_type, Boolean isNoobOrDoesNotExists)
        {
            if (isNoobOrDoesNotExists)
            {
                addToDictionary(variable_expression, new_variable_type, null);
                return false;
            }

            else
            {
                switch(new_variable_type)
                {
                    case "NUMBR":
                        if (old_variable_type.CompareTo("NUMBAR") == 0)
                        {
                            Double old_value;
                            Double.TryParse(old_variable_value, out old_value);
                            int new_variable_value = Convert.ToInt32(old_value);
                            addToDictionary(variable_expression, new_variable_type, new_variable_value.ToString());
                        }

                        else if (old_variable_type.CompareTo("YARN") == 0)
                        {
                            old_variable_value = trimYarnDoubleQuotes(old_variable_value);
                            Double old_value;
                            Boolean can_be_converted_to_number = Double.TryParse(old_variable_value, out old_value);

                            if (can_be_converted_to_number)
                            {
                                int new_variable_value = Convert.ToInt32(old_value);
                                addToDictionary(variable_expression, new_variable_type, new_variable_value.ToString());
                            }

                            else
                            {
                                this.main_form.promptError(9, null, 0);
                                return true;
                            }
                        }

                        else if(old_variable_type.CompareTo("TROOF") == 0)
                        {
                            if (old_variable_value.CompareTo("WIN") == 0)
                            {
                                int new_variable_value = 1;
                                addToDictionary(variable_expression, new_variable_type, new_variable_value.ToString());
                            }

                            else
                            {
                                int new_variable_value = 0;
                                addToDictionary(variable_expression, new_variable_type, new_variable_value.ToString());
                            }
                        }

                        break;

                    case "NUMBAR":
                        if (old_variable_type.CompareTo("NUMBR") == 0)
                        {
                            Double old_value;
                            Double.TryParse(old_variable_value, out old_value);
                            double new_variable_value = old_value;
                            addToDictionary(variable_expression, new_variable_type, new_variable_value.ToString());
                        }

                        else if (old_variable_type.CompareTo("YARN") == 0)
                        {
                            old_variable_value = trimYarnDoubleQuotes(old_variable_value);
                            Double old_value;
                            Boolean can_be_converted_to_number = Double.TryParse(old_variable_value, out old_value);

                            if (can_be_converted_to_number)
                            {
                                double new_variable_value = old_value;
                                addToDictionary(variable_expression, new_variable_type, new_variable_value.ToString());
                            }

                            else
                            {
                                this.main_form.promptError(9, null, 0);
                                return true;
                            }
                        }

                        else if (old_variable_type.CompareTo("TROOF") == 0)
                        {
                            if (old_variable_value.CompareTo("WIN") == 0)
                            {
                                double new_variable_value = 1;
                                addToDictionary(variable_expression, new_variable_type, new_variable_value.ToString());
                            }

                            else
                            {
                                double new_variable_value = 0;
                                addToDictionary(variable_expression, new_variable_type, new_variable_value.ToString());
                            }
                        }

                        break;

                    case "YARN":
                        if(old_variable_type.CompareTo("NUMBR") == 0 || old_variable_type.CompareTo("NUMBAR") == 0 || old_variable_type.CompareTo("TROOF") == 0)
                        {
                            String new_variable_value = "\"" + old_variable_value + "\"";
                            addToDictionary(variable_expression, new_variable_type, new_variable_value);
                        }

                        break;

                    case "TROOF":
                        if (old_variable_type.CompareTo("NUMBAR") == 0 || old_variable_type.CompareTo("NUMBR") == 0)
                        {
                            Double old_value;
                            Double.TryParse(old_variable_value, out old_value);
                            
                            String new_variable_value = "";
                            if (old_value == 0) new_variable_value = "0";
                            else new_variable_value = "1";
                        }

                        else if (old_variable_type.CompareTo("YARN") == 0)
                        { 
                            String new_variable_value = "";
                            if(System.Text.RegularExpressions.Regex.IsMatch(old_variable_value, @"^""\s*""$", RegexOptions.None)) new_variable_value = "0";
                            else new_variable_value = "1";

                            addToDictionary(variable_expression, new_variable_type, new_variable_value);
                        }

                        break;

                    default:
                        break;
                }

                return false;
            }
        }

        // this method performs the interpretation of concatenation operation
        private Boolean isConcatenation(String concatenation_expression_1, String concatenation_expression_2)
        {
            String variable_type;

            variable_type = checkType(concatenation_expression_1);
            if (variable_type.CompareTo("YARN") == 0) concatenation_expression_1 = trimYarnDoubleQuotes(concatenation_expression_1);
            else if (variable_type.CompareTo("NOOB") == 0)
            {
                concatenation_expression_1 = checkAndGetValueFromDictionary(concatenation_expression_1);

                // checks if the variable to exists
                if (concatenation_expression_1.CompareTo("NULL") == 0)
                {
                    this.main_form.promptError(18, null, 0);
                    return true;
                }

                // checks if the variable type is noob
                else if (concatenation_expression_1.CompareTo("NOOB") == 0)
                {
                    this.main_form.promptError(19, null, 0);
                    return true;
                }

                else
                {
                    variable_type = checkType(concatenation_expression_1);
                    if (variable_type.CompareTo("YARN") == 0) concatenation_expression_1 = trimYarnDoubleQuotes(concatenation_expression_1);
                }
            }

            variable_type = checkType(concatenation_expression_2);
            if (variable_type.CompareTo("YARN") == 0) concatenation_expression_2 = trimYarnDoubleQuotes(concatenation_expression_2);
            else if (variable_type.CompareTo("NOOB") == 0)
            {
                concatenation_expression_2 = checkAndGetValueFromDictionary(concatenation_expression_2);

                // checks if the variable exist
                if (concatenation_expression_2.CompareTo("NULL") == 0)
                {
                    this.main_form.promptError(18, null, 0);
                    return true;
                }

                // checks if the variable type is noob
                else if (concatenation_expression_2.CompareTo("NOOB") == 0)
                {
                    this.main_form.promptError(19, null, 0);
                    return true;
                }

                else
                {
                    variable_type = checkType(concatenation_expression_2);
                    if (variable_type.CompareTo("YARN") == 0) concatenation_expression_2 = trimYarnDoubleQuotes(concatenation_expression_2);
                }
            }

            String result_string = "\"" + concatenation_expression_1 + concatenation_expression_2 + "\"";

            addToDictionary("IT", null, result_string);

            return false;
        }

        // this method gets the indices of the YA RLY, NO WAI, and OIC statements
        private int getIndexIfThen(int i, Boolean isYaRly, Boolean isNoWai, Boolean isOIC)
        {
            if (isYaRly)
            {
                for (i = i; i < lexemes.Length; i++)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[i], @"^YA\s+RLY$", RegexOptions.None)) break;
                    else continue;
                }

                if (i < lexemes.Length) return i;
                else return -1;
            }

            else if (isNoWai)
            {
                for (i = i; i < lexemes.Length; i++)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[i], @"^NO\s+WAI$", RegexOptions.None)) break;
                    else continue;
                }

                if (i < lexemes.Length) return i;
                else return -1;
            }

            else if (isOIC)
            {
                for (i = i; i < lexemes.Length; i++)
                {
                    if (lexemes[i].CompareTo("OIC") == 0) break;
                    else continue;
                }

                if (i < lexemes.Length) return i;
                else return -1;
            }

            else return -1;
        }

        private int[] getMebbeIndices(int i, int oic_index)
        {
            List<int> mebbe_indices = new List<int>();
            for (i = i; i < oic_index; i++)
            {
                if (lexemes[i].CompareTo("MEBBE") == 0) mebbe_indices.Add(i);
            }

            if (!mebbe_indices.Any()) mebbe_indices.Clear();

            return mebbe_indices.ToArray();
        }

        // this method performs the interpreation of if-then statements
        private Boolean isIfThenStatement(Boolean if_then_condition, int ya_rly_index, int no_wai_index, int oic_index)
        {
            if (if_then_condition)
            {
                Boolean has_error = false;

                if (no_wai_index == -1) no_wai_index = oic_index;

                for (int i = ++ya_rly_index; i < no_wai_index; i++)
                {
                    i = interpretSubexpressionLexemes(i);
                    if (i == -1)
                    {
                        has_error = true; 
                        break;
                    }
                }

                if (has_error) return true;
                else return false;
            }

            else
            {
                if (no_wai_index == -1) return false;
                else
                {
                    Boolean has_error = false;

                    for (int i = ++no_wai_index; i < oic_index; i++)
                    {
                        i = interpretSubexpressionLexemes(i);
                        if (i == -1)
                        {
                            has_error = true;
                            break;
                        }
                    }

                    if (has_error) return true;
                    else return false;
                }
            }
        }

        // this method performs the interpreation of else-if statements
        private Boolean isIfThenStatementWithMebbe(Boolean if_then_condition, int ya_rly_index, int no_wai_index, int oic_index, int[] mebbe_indices)
        {
            if (if_then_condition)
            {
                Boolean has_error = false;

                for (int i = ++ya_rly_index; i < mebbe_indices[0]; i++)
                {
                    i = interpretSubexpressionLexemes(i);
                    if (i == -1)
                    {
                        has_error = true;
                        break;
                    }
                }

                if (has_error) return true;
                else return false;
            }

            else
            {
                int i;
                for (i = 0; i < mebbe_indices.Length; i++)
                {
                    int j = interpretSubexpressionLexemes(mebbe_indices[i] + 1);

                    String mebbe_expression_result = checkAndGetValueFromDictionary("IT");
                    Boolean mebbe_condition = false;

                    if (!String.IsNullOrEmpty(mebbe_expression_result) && mebbe_expression_result.CompareTo("WIN") == 0) mebbe_condition = true;
                    else mebbe_condition = false;

                    if (mebbe_condition)
                    {
                        Boolean has_error = false;
                        if (i + 1 == mebbe_indices.Length)
                        {
                            if (no_wai_index == -1) no_wai_index = oic_index;
                            for (int k = j + 1; k < no_wai_index; k++)
                            {
                                k = interpretSubexpressionLexemes(k);
                                if (k == -1)
                                {
                                    has_error = true;
                                    break;
                                }
                            }

                            if (has_error) return true;
                            else return false;
                        }

                        else
                        {
                            for (int k = j + 1; k < mebbe_indices[i + 1]; k++)
                            {
                                k = interpretSubexpressionLexemes(k);
                                if (k == -1)
                                {
                                    has_error = true;
                                    break;
                                }
                            }

                            if (has_error) return true;
                            else return false;
                        }
                    }

                    else continue;
                }

                if (i == mebbe_indices.Length)
                {
                    if (no_wai_index == -1) return false;
                    else
                    {
                        Boolean has_error = false;

                        for (int j = ++no_wai_index; j < oic_index; j++)
                        {
                            j = interpretSubexpressionLexemes(j);
                            if (j == -1)
                            {
                                has_error = true;
                                break;
                            }
                        }

                        if (has_error) return true;
                        else return false;
                    }
                }

                // for catching purposes only
                else return false;
            }
        }

        // this method gets the indices of OMG, GTFO, and OIC statements
        private switch_index[] getIndexSwitch(int i, Boolean isOMGGTFO, Boolean isOIC, Boolean isOMGWTF)
        {
            List<switch_index> indices = new List<switch_index>();

            if (isOIC)
            {
                for (i = i; i < lexemes.Length; i++)
                {
                    switch_index s = new switch_index();

                    if (lexemes[i].CompareTo("OIC") == 0)
                    {
                        s.index = i;
                        s.gtfo_index = -1;
                        indices.Add(s);
                        break;
                    }
                }

                if (indices == null || !indices.Any()) indices.Clear();
            }

            else if (isOMGWTF)
            {
                for (i = i; i < lexemes.Length; i++)
                {
                    switch_index s = new switch_index();

                    if (lexemes[i].CompareTo("OMGWTF") == 0)
                    {
                        s.index = i;
                        s.gtfo_index = -1;
                        indices.Add(s);
                        break;
                    }
                }

                if (indices == null || !indices.Any()) indices.Clear();
            }

            else if (isOMGGTFO)
            {
                for (i = i; i < lexemes.Length; i++)
                { 
                    switch_index s = new switch_index();

                    if (lexemes[i].CompareTo("OMG") == 0)
                    {
                        s.index = i;

                        for (int k = i + 1; k < lexemes.Length; k++)
                        {
                            if (lexemes[k].CompareTo("GTFO") == 0)
                            {
                                s.gtfo_index = k;
                                i = k;
                                break;
                            }

                            else if (lexemes[k].CompareTo("OMG") == 0 || lexemes[k].CompareTo("OIC") == 0)
                            {
                                s.gtfo_index = -1;
                                i = k - 1;
                                break;
                            }
                        }

                        indices.Add(s);
                    }

                    else if (lexemes[i].CompareTo("OIC") == 0) break;
                }

                if (indices == null || !indices.Any()) indices.Clear();
            }

            return indices.ToArray();
        }

        // this method performs the interpreation of switch statements
        private Boolean isSwitchStatement(String switch_control_statement, switch_index[] omggtfo_indices, int omgwtf_index)
        {
            Boolean has_been_matched = false;
            int i;

            for (i = 0; i < omggtfo_indices.Length; i++)
            {
                if (has_been_matched)
                {
                    for (int k = ((omggtfo_indices[i].index) + 2); k < lexemes.Length; k++)
                    {
                        if (lexemes[k].CompareTo("OMG") == 0 || lexemes[k].CompareTo("GTFO") == 0 || lexemes[k].CompareTo("OMGWTF") == 0 || lexemes[k].CompareTo("OIC") == 0) break;
                        else k = interpretSubexpressionLexemes(k);

                        if (k == -1) return true;
                    }

                    if (omggtfo_indices[i].gtfo_index != -1) break;
                }

                else
                {
                    if (switch_control_statement.CompareTo(lexemes[(omggtfo_indices[i].index) + 1]) == 0)
                    {
                        has_been_matched = true;

                        for (int k = ((omggtfo_indices[i].index) + 2); k < lexemes.Length; k++)
                        {
                            if (lexemes[k].CompareTo("OMG") == 0 || lexemes[k].CompareTo("GTFO") == 0 || lexemes[k].CompareTo("OMGWTF") == 0 || lexemes[k].CompareTo("OIC") == 0) break;
                            else k = interpretSubexpressionLexemes(k);

                            if (k == -1) return true;
                        }

                        if (omggtfo_indices[i].gtfo_index != -1) break;
                    }

                    else continue;
                }
            }

            if(omgwtf_index != -1 && i == omggtfo_indices.Length && has_been_matched)
            {
                if (omggtfo_indices[--i].gtfo_index == -1)
                {
                    for (int k = omgwtf_index + 1; k < lexemes.Length; k++)
                    {
                        if (lexemes[k].CompareTo("OIC") == 0) break;
                        else 
                        {
                            k = interpretSubexpressionLexemes(k);
                            if (k == -1) return true;
                        }
                    }
                }
            }

            else if(omgwtf_index != -1 && !has_been_matched)
            {
                for (int k = omgwtf_index + 1; k < lexemes.Length; k++)
                {
                    if (lexemes[k].CompareTo("OIC") == 0) break;
                    else
                    {
                        k = interpretSubexpressionLexemes(k);
                        if (k == -1) return true;
                    }
                }
            }

            return false;
        }

        // this method performs the implementation of loop operation
        private Boolean isLoop(int i, String loop_variable, Boolean isUppin, Boolean isNerfin, Boolean isTil, Boolean isWile, int im_outta_index)
        {
            int j = i;
            if (EXPRESSION.Match(lexemes[j]).Success)
            {
                if (COMPARISON_OPERATION_1.Match(lexemes[j]).Success)
                {
                    j = getLexemes(i, false, true, true, true);
                    Boolean comparison_error = isComparisonOperation();
                    if (comparison_error) return true;
                    else
                    {
                        loop_back:
                            j = getLexemes(i, false, true, true, true);
                            comparison_error = isComparisonOperation();

                            String control_statement = checkAndGetValueFromDictionary("IT");

                            for (j = j + 1; j < im_outta_index; j++)
                            {
                                j = interpretSubexpressionLexemes(j);
                                if (j == -1) return true;
                            }

                            String variable_value = checkAndGetValueFromDictionary(loop_variable);
                            String variable_type = checkType(variable_value);
                                
                            Double parsed_value;
                            Double.TryParse(variable_value, out parsed_value);
                            if (isUppin) ++parsed_value;
                            else if (isNerfin) --parsed_value;

                            addToDictionary(loop_variable, variable_type, parsed_value.ToString());

                            if (isWile && control_statement.CompareTo("WIN") == 0) goto loop_back;
                            else if (isTil && control_statement.CompareTo("FAIL") == 0) goto loop_back;
                            else return false;
                    }
                }
            }

            return false;
        }

        // this method interprets subexpression lexemes i.e. lexemes under the conditional statements
        private int interpretSubexpressionLexemes(int i)
        {
            String variable_name = "";

            if (VAR_DECLARATION.Match(lexemes[i]).Success)
            {
                variable_name = lexemes[++i];
                int j = i + 1;
                Boolean vardec_error = false;

                if (lexemes[j].CompareTo("ITZ") == 0)
                {
                    i = j;
                    String variable_value = "";

                    if (EXPRESSION.Match(lexemes[++i]).Success)
                    {
                        if (ARITHMETIC_OPERATION.Match(lexemes[i]).Success)
                        {
                            i = getLexemes(i, true, false, false, false);

                            Boolean arithmetic_operation_error = isArithmeticOperation();

                            if (arithmetic_operation_error) return -1;
                            else variable_value = checkAndGetValueFromDictionary("IT");
                        }

                        else if (COMPARISON_OPERATION_1.Match(lexemes[i]).Success)
                        {
                            i = getLexemes(i, false, true, false, false);

                            Boolean comparison_operation_error = isComparisonOperation();

                            if (comparison_operation_error) return -1;
                            else variable_value = checkAndGetValueFromDictionary("IT");
                        }

                        else if (BOOLEAN_OPERATION.Match(lexemes[i]).Success)
                        {
                            i = getLexemes(i, false, false, true, false);

                            Boolean boolean_operation_error = isBooleanOperation();

                            if (boolean_operation_error) return -1;
                            else variable_value = checkAndGetValueFromDictionary("IT");
                        }

                        else if (BOOLEAN_OPERATION_2.Match(lexemes[i]).Success)
                        {
                            i = getLexemes(i, false, false, false, true);

                            Boolean boolean_operation_error = isBooleanOperation2();

                            if (boolean_operation_error) return -1;
                            else variable_value = checkAndGetValueFromDictionary("IT");
                        }

                        else if (lexemes[i].CompareTo("SMOOSH") == 0)
                        {
                            String concatenation_expression_1, concatenation_expression_2;

                            concatenation_expression_1 = lexemes[++i];
                            if (lexemes[++i].CompareTo("AN") == 0) concatenation_expression_2 = lexemes[++i];
                            else concatenation_expression_2 = lexemes[i];

                            Boolean concatenation_error = isConcatenation(concatenation_expression_1, concatenation_expression_2);

                            if (concatenation_error) return -1;
                            else
                            {
                                variable_value = checkAndGetValueFromDictionary("IT");
                                ++i;
                            }
                        }
                    }

                    else variable_value = lexemes[i];

                    vardec_error = isVariableDeclaration(variable_name, variable_value, true);
                }

                else vardec_error = isVariableDeclaration(variable_name, null, false);


                if (vardec_error)
                {
                    this.main_form.promptError(2, variable_name, 0);
                    return -1;
                }

                else return i;
            }

            else if (lexemes[i].CompareTo("GIMMEH") == 0)
            {
                variable_name = lexemes[++i];
                getTextForBasicInput(variable_name);

                return i;
            }

            else if (lexemes[i].CompareTo("VISIBLE") == 0)
            {
                int j = ++i;
                Boolean basic_ouput_error = false;

                // checks if the variable to be printed to the console is an expression
                if (EXPRESSION.Match(lexemes[j]).Success)
                {
                    // if arithmetic operation
                    if (ARITHMETIC_OPERATION.Match(lexemes[j]).Success)
                    {
                        j = getLexemes(j, true, false, false, false);

                        Boolean arithmetic_operation_error = isArithmeticOperation();

                        if (arithmetic_operation_error) return -1;
                        else
                        {
                            variable_name = dictionary["IT"].value;
                            basic_ouput_error = isBasicOutput(variable_name);
                        }
                    }

                    // if comparison operation
                    else if (COMPARISON_OPERATION_1.Match(lexemes[j]).Success)
                    {
                        j = getLexemes(j, false, true, false, false);

                        Boolean comparison_operation_error = isComparisonOperation();

                        if (comparison_operation_error) return -1;
                        else
                        {
                            variable_name = checkAndGetValueFromDictionary("IT");
                            basic_ouput_error = isBasicOutput(variable_name);
                        }
                    }

                    // if boolean expression
                    else if (BOOLEAN_OPERATION.Match(lexemes[j]).Success)
                    {
                        j = getLexemes(j, false, false, true, false);

                        Boolean booelean_operation_error = isBooleanOperation();

                        if (booelean_operation_error) return -1;
                        else
                        {
                            variable_name = checkAndGetValueFromDictionary("IT");
                            basic_ouput_error = isBasicOutput(variable_name);
                        }
                    }

                    // if boolean expression
                    else if (BOOLEAN_OPERATION_2.Match(lexemes[j]).Success)
                    {
                        j = getLexemes(j, false, false, false, true);
                        Boolean booelean_operation_error = isBooleanOperation2();

                        if (booelean_operation_error) return -1;
                        else
                        {
                            variable_name = checkAndGetValueFromDictionary("IT");
                            basic_ouput_error = isBasicOutput(variable_name);
                        }
                    }

                    // if concatenation operation
                    else if (lexemes[j].CompareTo("SMOOSH") == 0)
                    {
                        String concatenation_expression_1, concatenation_expression_2;

                        concatenation_expression_1 = lexemes[++j];
                        if (lexemes[++j].CompareTo("AN") == 0) concatenation_expression_2 = lexemes[++j];
                        else concatenation_expression_2 = lexemes[j];

                        Boolean concatenation_error = isConcatenation(concatenation_expression_1, concatenation_expression_2);

                        if (concatenation_error) return -1;
                        else
                        {
                            variable_name = checkAndGetValueFromDictionary("IT");
                            basic_ouput_error = isBasicOutput(variable_name);
                            ++j;
                        }
                    }
                }

                // checks if variable
                else if (VAR_NAME.Match(lexemes[j]).Success)
                {
                    variable_name = lexemes[j];
                    basic_ouput_error = isBasicOutput(variable_name);
                }

                // checks if primitive data type
                else
                {
                    variable_name = lexemes[j];
                    basic_ouput_error = isBasicOutput(variable_name);
                }

                i = j;

                if (basic_ouput_error) return -1;
                else return i;
            }

            else if (ARITHMETIC_OPERATION.Match(lexemes[i]).Success)
            {
                int j = i;
                j = getLexemes(j, true, false, false, false);
                i = j;

                Boolean arithmetic_operation_error = false;
                arithmetic_operation_error = isArithmeticOperation();

                if (arithmetic_operation_error) return -1;
                else return i;
            }

            // comparison operation
            else if (COMPARISON_OPERATION_1.Match(lexemes[i]).Success)
            {
                int j = i;
                j = getLexemes(j, false, true, false, false);
                i = j;

                Boolean comparison_operation_error = false;
                comparison_operation_error = isComparisonOperation();

                if (comparison_operation_error) return -1;
                else return i;
            }

            // boolean operation
            else if (BOOLEAN_OPERATION.Match(lexemes[i]).Success)
            {
                int j = i;
                j = getLexemes(j, false, false, true, false);
                i = j;

                Boolean boolean_operation_error = false;
                boolean_operation_error = isBooleanOperation();

                if (boolean_operation_error) return -1;
                else return i;
            }

            // boolean operation
            else if (BOOLEAN_OPERATION_2.Match(lexemes[i]).Success)
            {
                int j = i;
                j = getLexemes(j, false, false, false, true);
                i = j;

                Boolean boolean_operation_error = false;
                boolean_operation_error = isBooleanOperation2();

                if (boolean_operation_error) return -1;
                else return i;
            }

            else if (TYPE_CASTING.Match(lexemes[i]).Success)
            {
                String variable_expression = lexemes[++i];

                String new_variable_type = "";

                if (lexemes[++i].CompareTo("A") == 0) new_variable_type = lexemes[++i];
                else new_variable_type = lexemes[i];

                String variable_value = checkAndGetValueFromDictionary(variable_expression);

                Boolean typecasting_error = false;

                if (variable_value.CompareTo("NOOB") == 0 || variable_value.CompareTo("NULL") == 0) typecasting_error = isTypecasting(variable_expression, null, null, new_variable_type, true);
                else
                {
                    String old_variable_type = checkType(variable_value);
                    typecasting_error = isTypecasting(variable_expression, variable_value, old_variable_type, new_variable_type, false);
                }

                if (typecasting_error) return -1;
                else return i;
            }

            // concatenation operation
            else if (lexemes[i].CompareTo("SMOOSH") == 0)
            {
                int j = i;
                String concatenation_expression_1, concatenation_expression_2;

                concatenation_expression_1 = lexemes[++j];
                if (lexemes[++j].CompareTo("AN") == 0) concatenation_expression_2 = lexemes[++j];
                else concatenation_expression_2 = lexemes[j];

                Boolean concatenation_error = isConcatenation(concatenation_expression_1, concatenation_expression_2);

                if (concatenation_error) return -1;
                else return ++j;
            }

            // if-then statment
            else if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[i], @"^O\s+RLY\?$", RegexOptions.None))
            {
                String condition = checkAndGetValueFromDictionary("IT");
                Boolean if_then_condition = false;

                if (!String.IsNullOrEmpty(condition) && condition.CompareTo("WIN") == 0) if_then_condition = true;
                else if_then_condition = false;

                int ya_rly_index = getIndexIfThen(i, true, false, false);
                int no_wai_index = getIndexIfThen(i, false, true, false);
                int oic_index = getIndexIfThen(i, false, false, true);
                int[] mebbe_indices = getMebbeIndices(i, oic_index);

                Boolean if_then_error = false;

                if (ya_rly_index == -1 || oic_index == -1)
                {
                    if (ya_rly_index == -1) this.main_form.promptError(9, null, 0);
                    if (oic_index == -1) this.main_form.promptError(11, null, 0);
                    return -i;
                }

                if (mebbe_indices != null && mebbe_indices.Length != 0) if_then_error = isIfThenStatementWithMebbe(if_then_condition, ya_rly_index, no_wai_index, oic_index, mebbe_indices);
                else if_then_error = isIfThenStatement(if_then_condition, ya_rly_index, no_wai_index, oic_index);

                if (if_then_error) return -1;
                else return oic_index;
            }

            else if (VAR_NAME.Match(lexemes[i]).Success)
            {
                int j = i + 1;
                variable_name = lexemes[i];

                // assignment operation
                if (lexemes[j].CompareTo("R") == 0)
                {
                    i = j;
                    String variable_expression = lexemes[++i];
                    Boolean assignment_operation_error = false;

                    if (NUMBR.Match(variable_expression).Success || NUMBAR.Match(variable_expression).Success || TROOF.Match(variable_expression).Success || YARN.Match(variable_expression).Success) assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, true, false, false);
                    else if (EXPRESSION.Match(variable_expression).Success)
                    {
                        // checks if arithmetic operation
                        if (ARITHMETIC_OPERATION.Match(variable_expression).Success)
                        {
                            i = getLexemes(i, true, false, false, false);

                            Boolean arithmetic_operation_error = false;
                            arithmetic_operation_error = isArithmeticOperation();

                            if (arithmetic_operation_error) return -1;
                            else
                            {
                                variable_expression = dictionary["IT"].value;
                                assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, false);
                            }
                        }

                        // checks if comparison operation
                        else if (COMPARISON_OPERATION_1.Match(variable_expression).Success)
                        {
                            i = getLexemes(i, false, true, false, false);

                            Boolean comparison_operation_error = isComparisonOperation();

                            if (comparison_operation_error) return -1;
                            else
                            {
                                variable_expression = checkAndGetValueFromDictionary("IT");
                                assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, false);
                            }
                        }

                            // checks if boolean operation
                        else if (BOOLEAN_OPERATION.Match(variable_expression).Success)
                        {
                            j = i;
                            j = getLexemes(j, false, false, true, false);

                            Boolean booelean_operation_error = isBooleanOperation();

                            if (booelean_operation_error) return -1;
                            else
                            {
                                variable_expression = checkAndGetValueFromDictionary("IT");
                                assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, true);
                            }

                            i = j;
                        }

                        // checks if boolean opearation
                        else if (BOOLEAN_OPERATION_2.Match(variable_expression).Success)
                        {
                            j = i;
                            j = getLexemes(j, false, false, false, true);
                            Boolean booelean_operation_error = isBooleanOperation2();

                            if (booelean_operation_error) return -1;
                            else
                            {
                                variable_expression = checkAndGetValueFromDictionary("IT");
                                assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, true);
                            }

                            i = j;
                        }

                        // checks if concatenation expression
                        else if (variable_expression.CompareTo("SMOOSH") == 0)
                        {
                            String concatenation_expression_1, concatenation_expression_2;

                            concatenation_expression_1 = lexemes[++i];
                            if (lexemes[++i].CompareTo("AN") == 0) concatenation_expression_2 = lexemes[++i];
                            else concatenation_expression_2 = lexemes[i];

                            Boolean concatenation_error = isConcatenation(concatenation_expression_1, concatenation_expression_2);

                            if (concatenation_error) return -1;
                            else
                            {
                                variable_expression = checkAndGetValueFromDictionary("IT");
                                assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, true, false);
                            }

                            ++i;
                        }
                    }
                    else if (VAR_NAME.Match(variable_expression).Success) assignment_operation_error = isAssignmentOperation(variable_name, variable_expression, false, false, true);

                    if (assignment_operation_error) return -1;
                    else return i;
                }

                // recasting
                else if (TYPE_CASTING.Match(lexemes[j]).Success)
                {
                    Boolean recasting_error = false;

                    if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[j], @"^IS\s+NOW\s+A$", RegexOptions.None))
                    {
                        String new_variable_type = lexemes[++j];
                        String old_variable_value = checkAndGetValueFromDictionary(variable_name);

                        if (old_variable_value.CompareTo("NOOB") == 0 || old_variable_value.CompareTo("NULL") == 0) recasting_error = isTypecasting(variable_name, null, null, new_variable_type, true);
                        else
                        {
                            String old_variable_type = checkType(old_variable_value);
                            recasting_error = isTypecasting(variable_name, old_variable_value, old_variable_type, new_variable_type, false);
                        }

                        if (recasting_error) return -1;
                        else return j;
                    }

                    else if (System.Text.RegularExpressions.Regex.IsMatch(lexemes[j], @"^R\s+MAEK$", RegexOptions.None))
                    {
                        String new_variable_type = "";

                        variable_name = lexemes[++j];
                        if (lexemes[++j].CompareTo("A") == 0) new_variable_type = lexemes[++j];
                        else new_variable_type = lexemes[j];

                        String old_variable_value = checkAndGetValueFromDictionary(variable_name);

                        if (old_variable_value.CompareTo("NOOB") == 0 || old_variable_value.CompareTo("NULL") == 0) recasting_error = isTypecasting(variable_name, null, null, new_variable_type, true);
                        else
                        {
                            String old_variable_type = checkType(old_variable_value);
                            recasting_error = isTypecasting(variable_name, old_variable_value, old_variable_type, new_variable_type, false);
                        }

                        if (recasting_error) return -1;
                        else return j;
                    }

                    // for catching purposes only
                    else return 0;
                }

                // switch statement
                else if (lexemes[j].CompareTo("WTF?") == 0)
                {
                    i = j;

                    String switch_control_statement = checkAndGetValueFromDictionary("IT");
                    if (!String.IsNullOrEmpty(switch_control_statement))
                    {
                        String variable_type = checkType(switch_control_statement);

                        if (variable_type.CompareTo("YARN") == 0)
                        {
                            switch_index[] omg_gtfo_indices = getIndexSwitch(i, true, false, false);
                            switch_index[] omgwtf_index = getIndexSwitch(i, false, false, true);
                            switch_index[] oic_index = getIndexSwitch(i, false, true, false);

                            if (omg_gtfo_indices.Length == 0 || omg_gtfo_indices == null || oic_index.Length == 0 || oic_index == null)
                            {
                                if ((omg_gtfo_indices.Length == 0 || omg_gtfo_indices == null))
                                {
                                    this.main_form.promptError(13, null, 0);
                                    return -1;
                                }

                                else if ((oic_index.Length == 0 || oic_index == null))
                                {
                                    this.main_form.promptError(14, null, 0);
                                    return -1;
                                }

                                // for catching purposes only
                                else return 0;
                            }

                            else
                            {
                                Boolean switch_error = false;

                                switch_error = isSwitchStatement(switch_control_statement, omg_gtfo_indices, (omgwtf_index == null || omgwtf_index.Length == 0) ? -1 : omgwtf_index[0].index);

                                if (switch_error) return -1;
                                else return oic_index[0].index;
                            }

                        }

                        else
                        {
                            this.main_form.promptError(12, null, 0);
                            return -1;
                        }
                    }

                    else
                    {
                        this.main_form.promptError(12, null, 0);
                        return -1;
                    }
                }

                // for catching purposes only
                else return 0;
            }

            // for catching purposes only
            else return 0;
        }

        // this method checks the type of the variable and returns it the program
        private String checkType(String type)
        {
            if (NUMBAR.Match(type).Success) return "NUMBAR";
            else if (NUMBR.Match(type).Success) return "NUMBR";
            else if (TROOF.Match(type).Success) return "TROOF";
            else if (YARN.Match(type).Success) return "YARN";
            else return "NOOB";
        }

        // this method adds to the dictionary regardless if it the key is new or already existing
        private void addToDictionary(String variable_name, String variable_type, String variable_value)
        {
            if (dictionary.ContainsKey(variable_name) == true) dictionary[variable_name] = new type_value {type = variable_type, value = variable_value };
            else dictionary.Add(variable_name, new type_value { type = variable_type, value = variable_value });

            this.main_form.clearSymboltable();
            foreach (KeyValuePair<string, type_value> v in dictionary)
            {
                //if (v.Key.CompareTo("IT") == 0) continue;
                this.main_form.updateSymbolTable(v.Key, v.Value.type, v.Value.value);
            }
        }

        // this method trims the double quotes enclosing yarn variables
        private String trimYarnDoubleQuotes(String variable_name)
        {
            variable_name = variable_name.TrimEnd('"');
            variable_name = variable_name.TrimStart('"');
            return variable_name;
        }

        // this method checks and gets the value of the variable
        private String checkAndGetValueFromDictionary(String variable_name)
        {
            // checks if the variable exists in the dictionary
            if (dictionary.ContainsKey(variable_name) == true)
            {
                String variable_value = "";

                if (variable_name.CompareTo("IT") == 0)
                {
                    variable_value = dictionary["IT"].value;
                    if (String.IsNullOrEmpty(variable_value)) variable_value = "";
                }

                else
                {
                    variable_value = dictionary[variable_name].value;
                    if (dictionary[variable_name].type.CompareTo("NOOB") == 0) variable_value = "NOOB";
                    if (String.IsNullOrEmpty(variable_value)) variable_value = "";
                }
   
                return variable_value;
            }

            else return "NULL";
        }   
    }
}
 
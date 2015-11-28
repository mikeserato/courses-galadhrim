/*
 * 
 * 
 * 
 * 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        // Declaration of Regex for pattern matching
        /***  START OF EXPRESSIONS ***********************************************************************************************/
        // Basic Expression
        static Regex VAR_NAME = new Regex(@"[A-Za-z][A-Za-z0-9_]*", RegexOptions.None);
        static Regex NUMBR = new Regex(@"(-)?[0-9]+", RegexOptions.None);
        static Regex NUMBAR = new Regex(@"(-)?[0-9]+\.[0-9]+", RegexOptions.None);
        static Regex TROOF = new Regex(@"(WIN|FAIL)", RegexOptions.None);
        static Regex YARN = new Regex(@".+", RegexOptions.None);

        static Regex EXPRESSION = new Regex(@"(" + TROOF + "|" + VAR_NAME + "|" + NUMBR + "|" + NUMBAR + "|" + YARN + @")", RegexOptions.None);

        // Arithmetic Operation Expressions
        static Regex ADDITION_EXPRESSION = new Regex(@"(?<addition_keyword>SUM\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))");
        static Regex SUBTRACTION_EXPRESSION = new Regex(@"(?<subtraction_keyword>DIFF\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))");
        static Regex MULTIPLICATION_EXPRESSION = new Regex(@"(?<multiplication_keyword>PRODUKT\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))");
        static Regex QUOSHUNT_EXPRESSION = new Regex(@"(?<division_keyword>QUOSHUNT\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))");
        static Regex MODULO_EXPRESSION = new Regex(@"(?<modulo_keyword>MOD\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))");
        static Regex BIGGR_EXPRESSION = new Regex(@"(?<bigger_keyword>BIGGR\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))");
        static Regex SMALLR_EXPRESSION = new Regex(@"(?<smaller_keyword>SMALLR\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + TROOF + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))");

        static Regex BOOLEAN_EXPRESSION = new Regex(@"(((?<boolean_operation_1>(BOTH\s+OF|EITHER\s+OF|WON\s+OF))\s+(?<boolean_variable_2>" + EXPRESSION + @")\s+(?<boolean_keyword>AN\s+)?(?<boolean_variable_3>" + EXPRESSION + @")$))", RegexOptions.None);
        static Regex BOOLEAN_EXPRESSION_2 = new Regex(@"(?<boolean_not>NOT)\s+(?<boolean_variable>" + EXPRESSION + ")");

        // Comparison Expression
        static Regex COMPARISION_EXPRESSION = new Regex(@"(" + TROOF + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + "|" + VAR_NAME + "|" + NUMBAR + "|" + NUMBR + "|" + YARN + ")", RegexOptions.None);
        static Regex COMPARISON_1_EXPRESSION = new Regex(@"((?<mebbe_keyword>MEBBE)\s+)?(?<comparison_keyword>(BOTH\s+SAEM|DIFFRINT))\s+(?<comparison_expression_1>" + COMPARISION_EXPRESSION + @")\s+((?<comparison_noise_word>AN)\s+)?(?<comparison_expression_2>" + COMPARISION_EXPRESSION + ")$", RegexOptions.None);
        static Regex COMPARISON_2_EXPRESSION = new Regex(@"((?<mebbe_keyword>MEBBE)\s+)?(?<comparison_keyword_1>(BOTH\s+SAEM|DIFFRINT))\s+(?<comparison_expression_1>" + COMPARISION_EXPRESSION + @")\s+(?<comparison_keyword_2>(AND\s+BIGGR\s+OF|AND\s+SMALLR\s+OF))\s+(?<comparison_expression_2>" + COMPARISION_EXPRESSION + @")\s+(?<comparison_noise_word>AN)\s+(?<comparison_expression_3>" + COMPARISION_EXPRESSION + ")$", RegexOptions.None);

        // Casting Expression
        static Regex TYPECAST_EXPRESSION = new Regex(@"(" + VAR_NAME + ")", RegexOptions.None);

        // Concaternation Expression
        static Regex CONCATENATION_EXPRESSION_1 = new Regex(@"(" + VAR_NAME + "|" + YARN + "|" + NUMBR + "|" + NUMBAR + "|" + TROOF + ")", RegexOptions.None);
        static Regex CONCATENATION_EXPRESSION_2 = new Regex(@"(?<concatenation_keyword_1>SMOOSH)\s+(?<concatenation_expression_1>" + CONCATENATION_EXPRESSION_1 + @")\s+(?<concatenation_noise_word>AN\s+)?(?<concatenation_expression_2>" + CONCATENATION_EXPRESSION_1 + @")\s+(?<concatenation_keyword_2>MKAY)", RegexOptions.None);

        // Visible Expression
        static Regex VISIBLE_EXPRESSION = new Regex(@"(" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + "|" + YARN + "|" + NUMBAR + "|" + NUMBR + "|" + CONCATENATION_EXPRESSION_2 + "|" + VAR_NAME + "|" + COMPARISON_1_EXPRESSION + "|" + COMPARISON_2_EXPRESSION + "|" + BOOLEAN_EXPRESSION + "|" + BOOLEAN_EXPRESSION_2 + @")", RegexOptions.None);

        // Assignment Expression
        static Regex ASSIGNMENT_EXPRESSION = new Regex(@"(" + VAR_NAME + "|" + NUMBAR + "|" + NUMBR + "|" + TROOF + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + "|" + COMPARISON_1_EXPRESSION + "|" + COMPARISON_2_EXPRESSION + "|" + CONCATENATION_EXPRESSION_2 + "|" + BOOLEAN_EXPRESSION + "|" + BOOLEAN_EXPRESSION_2 + ")", RegexOptions.None);
        /***  END OF EXPRESSIONS ************************************************************************************************/

        /*** START OF LOLCODE SPECIFICATIONS REGEX ******************************************************************************/
        static Regex PROGRAM_DELIMITER = new Regex(@"\s*(?<program_delimiter>(HAI|KTHXBYE))\s*((?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)", RegexOptions.None);
        static Regex VAR_DECLARATION = new Regex(@"^(?<vardec>I\s+HAS\s+A)\s+(?<identifier>" + VAR_NAME + @")(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);
        static Regex VAR_DECLARATION_WITH_INITIALIZATION = new Regex(@"^(?<vardec>I\s+HAS\s+A)\s+(?<identifier>" + VAR_NAME + @")\s+(?<vardec_keyword>ITZ)\s+(?<variable_type>(" + NUMBAR + "|" + NUMBR + "|" + YARN + "|" + TROOF + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + CONCATENATION_EXPRESSION_2 + "|" + COMPARISON_1_EXPRESSION + "|" + COMPARISON_2_EXPRESSION + @"))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);

        static Regex COMMENT = new Regex(@"^(?<comment_keyword>BTW)\s+(?<comment_text>.*)\s*", RegexOptions.None);
        static Regex COMMENT_EXPRESSION = new Regex(@"(?<comment_keyword>BTW)\s+(?<comment_text>.*)\s$*", RegexOptions.None);

        static Regex BOOLEAN_OPERATION = new Regex(@"((^((?<mebbe_keyword>MEBBE)\s+)?(?<boolean_operation_1>(BOTH\s+OF|EITHER\s+OF|WON\s+OF))\s+(?<boolean_variable_2>" + EXPRESSION + @")\s+(?<boolean_keyword>AN\s+)?(?<boolean_variable_3>" + EXPRESSION + @")(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$))", RegexOptions.None);
        static Regex BOOLEAN_OPERATION_2 = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<boolean_not>NOT)\s+(?<boolean_variable>" + EXPRESSION + @")(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)");
        static Regex LOOP = new Regex(@"((^(?<loop_declaration>IM\s+IN\s+YR)\s+(?<loop_label>" + VAR_NAME + @")\s+(?<loop_operation>(UPPIN|NERFIN))\s+(?<loop_keyword_1>YR)\s+(?<loop_variable>" + VAR_NAME + @")(\s+(?<loop_keyword_2>(TIL|WILE))\s+(?<loop_expression>(" + COMPARISON_1_EXPRESSION + "|" + COMPARISON_2_EXPRESSION + @")))?(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$)|(^(?<loop_exit>IM\s+OUTTA\s+YR)\s+(?<loop_label_exit>" + VAR_NAME + @")(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$)|(?<loop_keyword_3>^GTFO)(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$)", RegexOptions.None);
        // IM IN YR LOOP UPPIN YR N WILE DIFFRINT N AN SMALLR OF N AN 10

        // Basic Input Operation
        static Regex BASIC_INPUT = new Regex(@"^(?<basic_input_keyword>GIMMEH)\s+(?<basic_input_variable>" + VAR_NAME + @")(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);

        // Assignment Operation
        static Regex ASSIGNMENT_OPERATION = new Regex(@"^(?<variable_name>" + VAR_NAME + @")\s+(?<assignment_keyword>R)\s+(?<assignment_expression>" + ASSIGNMENT_EXPRESSION + @")(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);

        // Comparison Operation
        static Regex COMPARISON_1 = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<comparison_keyword>(BOTH\s+SAEM|DIFFRINT))\s+(?<comparison_expression_1>" + COMPARISION_EXPRESSION + @")\s+((?<comparison_noise_word>AN)\s+)?(?<comparison_expression_2>" + COMPARISION_EXPRESSION + @")(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);
        static Regex COMPARISON_2 = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<comparison_keyword_1>(BOTH\s+SAEM|DIFFRINT))\s+(?<comparison_expression_1>" + COMPARISION_EXPRESSION + @")\s+(?<comparison_keyword_2>(AND\s+BIGGR\s+OF|AND\s+SMALLR\s+OF))\s+(?<comparison_expression_2>" + COMPARISION_EXPRESSION + @")\s+(?<comparison_noise_word>AN)\s+(?<comparison_expression_3>" + COMPARISION_EXPRESSION + @")(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);

        // Basic Output Operation
        static Regex VISIBLE = new Regex(@"^(?<basic_output_keyword>VISIBLE)\s+(?<expression>(" + VISIBLE_EXPRESSION + @"))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);

        //Arithmetic Operatios
        static Regex ADDITION = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<addition_keyword>SUM\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$");
        static Regex SUBTRACTION = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<subtraction_keyword>DIFF\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$");
        static Regex MULTIPLICATION = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<multiplication_keyword>PRODUKT\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$");
        static Regex QUOSHUNT = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<division_keyword>QUOSHUNT\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$");
        static Regex MODULO = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<modulo_keyword>MOD\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$");
        static Regex BIGGR = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<bigger_keyword>BIGGR\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$");
        static Regex SMALLR = new Regex(@"^((?<mebbe_keyword>MEBBE)\s+)?(?<smaller_keyword>SMALLR\s+OF)\s+(?<operand_1>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))\s+(?<separator>AN)\s+(?<operand_2>(" + NUMBR + "|" + NUMBAR + "|" + VAR_NAME + "|" + YARN + "|" + ADDITION_EXPRESSION + "|" + SUBTRACTION_EXPRESSION + "|" + MULTIPLICATION_EXPRESSION + "|" + QUOSHUNT_EXPRESSION + "|" + MODULO_EXPRESSION + "|" + BIGGR_EXPRESSION + "|" + SMALLR_EXPRESSION + @"))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$");

        // Concatenation Operation
        static Regex CONCATENATION = new Regex(@"^(?<concatenation_keyword_1>SMOOSH)\s+(?<concatenation_expression_1>" + CONCATENATION_EXPRESSION_1 + @")\s+(?<concatenation_noise_word>AN\s+)?(?<concatenation_expression_2>" + CONCATENATION_EXPRESSION_1 + @")\s+(?<concatenation_keyword_2>MKAY)(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);

        //If-then Conditionals
        static Regex IFTHEN_KEYWORDS = new Regex(@"\s*(?<ifthen_keywords>(O\s+RLY\?|YA\s+RLY|NO\s+WAI))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)");

        // Type casting and recasting
        static Regex TYPECAST = new Regex(@"^(?<typecasting_keyword_1>MAEK)\s+(?<typecasting_expression>" + TYPECAST_EXPRESSION + @")\s+(?<typecasting_keyword_2>A\s+)?(?<variable_type>(TROOF|YARN|NUMBR|NUMBAR|NOOB))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);
        static Regex RECAST_1 = new Regex(@"^(?<variable_name>" + VAR_NAME + @")\s+(?<recasting_keyword>IS\s+NOW\s+A)\s+(?<variable_type>(TROOF|YARN|NUMBR|NUMBAR|NOOB))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);
        static Regex RECAST_2 = new Regex(@"^(?<variable_name_1>" + VAR_NAME + @")\s+(?<recasting_keyword>R\s+MAEK)\s+(?<variable_name_2>" + VAR_NAME + @")\s+((?<recasting_noise_word>A)\s+)?(?<variable_type>(TROOF|YARN|NUMBR|NUMBAR|NOOB))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)$", RegexOptions.None);

        //Case Conditionals
        static Regex CASE_CONDITIONAL_KEYWORD = new Regex(@"\s*((?<case_keyword>(WTF\?|OMGWTF|GTFO))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)|(?<var_ident>" + VAR_NAME + @"),\s*(?<case_keyword>WTF\?))(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)");
        static Regex CASE_CONDITIONAL_CASES = new Regex(@"\s*(?<case_keyword>OMG)\s+(?<literal>" + YARN + @")(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)");

        static Regex TERMINATOR = new Regex(@"(?<terminator_keyword>OIC)(\s*(?<comment_line>(?<comment_keyword>BTW)\s+(?<comment_text>.*))?)");
        static Regex MULTILINE_COMMENT = new Regex(@"\s*(?<multi_comment>(OBTW|TLDR))");

        // Functions
        static Regex FUNCTION_1 = new Regex(@"\s*(?<function_delimiter>HOW\s+IZ\s+I)\s+(?<function_name>[a-zA-Z][a-zA-Z0-9_]*)(\s+(?<function_keyword_1>YR)\s+(?<argument_1>[a-zA-Z][a-zA-Z0-9]*)(\s+(?<function_noise_word>AN)\s+(?<function_keyword_2>YR)\s+(?<argument_2>[a-zA-Z][a-zA-Z0-9]*))*)?");
        static Regex FUNCTION_2 = new Regex(@"\s*(?<function_delimiter>IF\s+U\s+SAY\s+SO$)");
        /*** END OF LOLCODE SPECIFICATIONS REGEX ********************************************************************************/

        Boolean multi_comment = false;

        public Form1()
        {
            InitializeComponent();

            //tab names of the TabControl Tool
            tabControl1.TabPages[0].Text = "LEXEMES";
            tabControl1.TabPages[1].Text = "SYMBOL TABLE";
        }

        private void oPENToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //opening a file, the starting directory is the users desktop
            Stream myStream;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //setting the directory
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.RestoreDirectory = true;

            //opening a dialog box
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    string strfilename = openFileDialog1.FileName;
                    string filetext = File.ReadAllText(strfilename);
                    input_console.Text = filetext;
                }
            }

        }

        // this button is for the interpret option
        private void button2_Click(object sender, EventArgs e)
        {
            // clearing first the textbox and the datagridview
            output_console.Clear();
            symbol_table.Rows.Clear();
            lexemes_table.Rows.Clear();

            Boolean no_error = true;

            //spitting the whole code per line
            String input = input_console.Text;
            String[] code_lines = input.Split(new String[] { "\n" }, StringSplitOptions.None);
            code_lines = (from line in code_lines select line.Trim()).ToArray();

            foreach (String line in code_lines)
            {
                int line_index = Array.IndexOf(code_lines, line) + 1;

                //This checks if the line of code is a comment
                if (multi_comment)
                {
                    if (MULTILINE_COMMENT.Match(line).Success)
                    {
                        isMultilineComment(line);
                    }
                    else
                    {
                        lexemes_table.Rows.Add(line, "Comment");
                    }
                }
                else
                {
                    // checks if the line is a variable declaration statement
                    if (VAR_DECLARATION.Match(line).Success)
                    {
                        isVariableDeclaration(line, false);
                    }

                    // checks if the line is a variable declaration with initialization statement
                    else if (VAR_DECLARATION_WITH_INITIALIZATION.Match(line).Success)
                    {
                        isVariableDeclaration(line, true);
                    }

                    // checks if the line is a comment
                    else if (COMMENT.Match(line).Success)
                    {
                        isComment(line);
                    }

                    // checks if the line is an assignment operation statement
                    else if (ASSIGNMENT_OPERATION.Match(line).Success)
                    {
                        isAssignment(line);
                    }

                    // checks if the line is a boolean operation statemnt
                    else if (BOOLEAN_OPERATION.Match(line).Success)
                    {
                        isBooleanOperation(line);
                    }

                    else if (BOOLEAN_OPERATION_2.Match(line).Success)
                    {
                        isBooleanOperation2(line);
                    }

                    // checks if the line is a comparison operation statement
                    else if (COMPARISON_1.Match(line).Success || COMPARISON_2.Match(line).Success)
                    {
                        isComparisonOperation(line);
                    }

                    // checks if the line is a basic input operation statement
                    else if (BASIC_INPUT.Match(line).Success)
                    {
                        isBasicInput(line);
                    }

                    // checks if the line is a basic output operation statement
                    else if (VISIBLE.Match(line).Success)
                    {
                        isVisible(line);
                    }

                    // checks if the line is a casting operation statement
                    else if (TYPECAST.Match(line).Success)
                    {
                        isTypeCasting(line);
                    }

                    // checks if the line is a recasting operation statement
                    else if (RECAST_1.Match(line).Success || RECAST_2.Match(line).Success)
                    {
                        isTypeCasting(line);
                    }

                    // checks if the line is a addition operation statementc
                    else if (ADDITION.Match(line).Success)
                    {
                        isAddition(line);
                    }

                    // checks if the line is a subtraction operation statement
                    else if (SUBTRACTION.Match(line).Success)
                    {
                        isSubtraction(line);
                    }

                    // checks if the line is a multiplication operation statement
                    else if (MULTIPLICATION.Match(line).Success)
                    {
                        isMultiplication(line);
                    }

                    // checks if the line is a division operation statement
                    else if (QUOSHUNT.Match(line).Success)
                    {
                        isDivision(line);
                    }

                    // checks if the line is a modulo operation statement
                    else if (MODULO.Match(line).Success)
                    {
                        isModulo(line);
                    }

                    // checks if the line is a bigger operation statement
                    else if (BIGGR.Match(line).Success)
                    {
                        isBigger(line);
                    }

                    // checks if the line is a smaller operation statement
                    else if (SMALLR.Match(line).Success)
                    {
                        isSmaller(line);
                    }

                    // checks if the line is a concatenation operation statement
                    else if (CONCATENATION.Match(line).Success)
                    {
                        isConcatenation(line);
                    }

                    else if (IFTHEN_KEYWORDS.Match(line).Success)
                    {
                        isIfThenKeyword(line);
                    }

                    else if (CASE_CONDITIONAL_KEYWORD.Match(line).Success)
                    {
                        isCaseConditionalKeyword(line);
                    }
                    else if (CASE_CONDITIONAL_CASES.Match(line).Success)
                    {
                        isCaseConditionalCase(line);
                    }

                    else if (LOOP.Match(line).Success)
                    {
                        isLoop(line);
                    }
                    else if (PROGRAM_DELIMITER.Match(line).Success)
                    {
                        isProgramDelimiter(line);
                    }

                    else if (TERMINATOR.Match(line).Success)
                    {
                        isTerminator(line);
                    }

                    else if (MULTILINE_COMMENT.Match(line).Success)
                    {
                        isMultilineComment(line);
                    }

                    else if (FUNCTION_1.Match(line).Success || FUNCTION_2.Match(line).Success)
                    {
                        isFunction(line);
                    }

                    else
                    {
                        if (String.IsNullOrEmpty(line) || String.IsNullOrWhiteSpace(line)) continue;
                        else
                        {
                            promptError(1, null, line_index);
                            no_error = false;
                            break;
                        }
                    }
                }
            }

            // if no error is found in the lexical analysis, the program will proceed to the "interpretation" of the lolcode
            if (no_error)
            {
                List<String> lex = new List<String>();
                Boolean checkCmnt = false;

                foreach (DataGridViewRow row in lexemes_table.Rows)
                {
                    if (row.Cells[0].Value == null || row.Cells[0].Value.Equals("")) break;

                    String data = row.Cells[0].Value.ToString();
                    data = data.Trim();

                    if (data.CompareTo("BTW") == 0)
                    {
                        checkCmnt = true;
                        continue;
                    }
                    else
                    {
                        if (checkCmnt == false)
                        {
                            checkCmnt = false;
                            lex.Add(data);
                            if (data.CompareTo("KTHXBYE") == 0) break;
                            continue;
                        }
                        else
                        {
                            checkCmnt = false;
                            continue;
                        }
                    }
                }

                String[] lexemes = lex.ToArray();

                if (lexemes[0].CompareTo("HAI") == 0 && lexemes[lexemes.Length - 1].CompareTo("KTHXBYE") == 0)
                {
                    Interpreter i = new Interpreter(this, lexemes);
                    i.startInterpretation();
                }
                else promptError(0, null, 0);
            }
        }

        //this method is for variable declaration checking and putting it to the lexemes table
        private void isVariableDeclaration(String str, Boolean is_initialized)
        {
            if (is_initialized)
            {
                String variable_declaration_keyword = VAR_DECLARATION_WITH_INITIALIZATION.Match(str).Groups["vardec"].Value.ToString();
                String identifier = VAR_DECLARATION_WITH_INITIALIZATION.Match(str).Groups["identifier"].Value.ToString();
                String variable_declaration_auxiliary_keyword = VAR_DECLARATION_WITH_INITIALIZATION.Match(str).Groups["vardec_keyword"].Value.ToString();
                String variable_value = VAR_DECLARATION_WITH_INITIALIZATION.Match(str).Groups["variable_type"].Value.ToString();

                lexemes_table.Rows.Add(variable_declaration_keyword, "Variable Declaration Keyword");
                lexemes_table.Rows.Add(identifier, "Variable Identifier");
                lexemes_table.Rows.Add(variable_declaration_auxiliary_keyword, "Variable Declaration Keyword");

                checkExpression(variable_value);

                if (VAR_DECLARATION_WITH_INITIALIZATION.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = VAR_DECLARATION_WITH_INITIALIZATION.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);
                }
            }

            else
            {
                String variable_declaration_keyword = VAR_DECLARATION.Match(str).Groups["vardec"].Value.ToString();
                String identifier = VAR_DECLARATION.Match(str).Groups["identifier"].Value.ToString();

                lexemes_table.Rows.Add(variable_declaration_keyword, "Variable Declaration Keyword");
                lexemes_table.Rows.Add(identifier, "Variable Identifier");

                if (VAR_DECLARATION.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = VAR_DECLARATION.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);
                }

            }
        }

        //this method is for one-line comment checking and putting it to the lexemes table
        private void isComment(String str)
        {
            String comment_keyword = COMMENT.Match(str).Groups["comment_keyword"].Value.ToString();
            String comment_text = COMMENT.Match(str).Groups["comment_text"].Value.ToString();

            lexemes_table.Rows.Add(comment_keyword, "Comment Keyword");
            lexemes_table.Rows.Add(comment_text, "Comment");
        }

        //this method is for Basic input checking and putting it to the lexemes table
        private void isBasicInput(String str)
        {
            String basic_input_keyword = BASIC_INPUT.Match(str).Groups["basic_input_keyword"].Value.ToString();
            String basic_input_variable = BASIC_INPUT.Match(str).Groups["basic_input_variable"].Value.ToString();


            lexemes_table.Rows.Add(basic_input_keyword, "Basic Input Keyword");
            lexemes_table.Rows.Add(basic_input_variable, "Variable Identifier");

            if (BASIC_INPUT.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = BASIC_INPUT.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for assignment statements checking and putting it to the lexemes table
        private void isAssignment(String str)
        {
            String variable_identifier = ASSIGNMENT_OPERATION.Match(str).Groups["variable_name"].Value.ToString();
            String assignment_keyword = ASSIGNMENT_OPERATION.Match(str).Groups["assignment_keyword"].Value.ToString();
            String expression = ASSIGNMENT_OPERATION.Match(str).Groups["assignment_expression"].Value.ToString();

            lexemes_table.Rows.Add(variable_identifier, "Variable Identifier");
            lexemes_table.Rows.Add(assignment_keyword, "Assignment Keyword");
            checkExpression(expression);

            if (ASSIGNMENT_OPERATION.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = ASSIGNMENT_OPERATION.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for loop checking and putting it to the lexemes table
        private void isLoop(String str)
        {
            String loop_declaration = LOOP.Match(str).Groups["loop_declaration"].Value.ToString();
            String loop_label = LOOP.Match(str).Groups["loop_label"].Value.ToString();
            String loop_operation = LOOP.Match(str).Groups["loop_operation"].Value.ToString();
            String loop_keyword_1 = LOOP.Match(str).Groups["loop_keyword_1"].Value.ToString();
            String loop_variable = LOOP.Match(str).Groups["loop_variable"].Value.ToString();
            String loop_keyword_2 = LOOP.Match(str).Groups["loop_keyword_2"].Value.ToString();
            String loop_expression = LOOP.Match(str).Groups["loop_expression"].Value.ToString();
            String loop_exit = LOOP.Match(str).Groups["loop_exit"].Value.ToString();
            String loop_label_exit = LOOP.Match(str).Groups["loop_label_exit"].Value.ToString();
            String loop_keyword_3 = LOOP.Match(str).Groups["loop_keyword_3"].Value.ToString();


            // IM IN YR <loop_label> <loop_operation> YR <loop_variable>
            if (!String.IsNullOrEmpty(loop_declaration))
            {
                lexemes_table.Rows.Add(loop_declaration, "Loop Keyword");
                lexemes_table.Rows.Add(loop_label, "Loop Label");
                lexemes_table.Rows.Add(loop_operation, "Loop Operation");
                lexemes_table.Rows.Add(loop_keyword_1, "Loop Keyword");
                lexemes_table.Rows.Add(loop_variable, "Variable Identifier");

                // IM IN YR <loop_label> <loop_operation> YR <loop_variable> <loop_condition> <loop_expression>
                if (!String.IsNullOrEmpty(loop_keyword_2))
                {
                    lexemes_table.Rows.Add(loop_keyword_2, "Loop Keyword");
                    checkExpression(loop_expression);

                    if (LOOP.Match(str).Groups["comment_line"].Value != "")
                    {
                        String comment = LOOP.Match(str).Groups["comment_line"].Value.ToString();
                        checkExpression(comment);
                    }

                }
            }

            // IM OUTTA YR <loop_label>
            else if (!String.IsNullOrEmpty(loop_exit))
            {
                lexemes_table.Rows.Add(loop_exit, "Loop Keyword");
                lexemes_table.Rows.Add(loop_label_exit, "Loop Label");

                if (LOOP.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = LOOP.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);
                }
            }

            // GTFO
            else if (!String.IsNullOrEmpty(loop_keyword_3))
            {
                lexemes_table.Rows.Add(loop_keyword_3, "Loop Keyword");
                if (LOOP.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = LOOP.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);
                }
            }
        }

        // this method is for boolean operation checking and putting it to the lexemes table
        private void isBooleanOperation(String str)
        {
            if (BOOLEAN_OPERATION.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = BOOLEAN_OPERATION.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            String boolean_operation_1 = BOOLEAN_OPERATION.Match(str).Groups["boolean_not"].Value.ToString();
            String boolean_variable_1 = BOOLEAN_OPERATION.Match(str).Groups["boolean_variable_1"].Value.ToString();
            String boolean_operation_2 = BOOLEAN_OPERATION.Match(str).Groups["boolean_operation_1"].Value.ToString();
            String boolean_variable_2 = BOOLEAN_OPERATION.Match(str).Groups["boolean_variable_2"].Value.ToString();
            String boolean_keyword = BOOLEAN_OPERATION.Match(str).Groups["boolean_keyword"].Value.ToString();
            String boolean_variable_3 = BOOLEAN_OPERATION.Match(str).Groups["boolean_variable_3"].Value.ToString();

            // NOT OF <boolean_variable>
            if (!String.IsNullOrEmpty(boolean_operation_1))
            {
                lexemes_table.Rows.Add(boolean_operation_1, "Boolean Operation");
                checkExpression(boolean_variable_1);
                //lexemes_table.Rows.Add(boolean_variable_1, "Variable Identifier");
            }

            // (BOTH OF|EITHER OF|WON OF) <boolean_variable> AN <boolean_variable>
            else if (!String.IsNullOrEmpty(boolean_operation_2))
            {
                lexemes_table.Rows.Add(boolean_operation_2, "Boolean Operation");
                checkExpression(boolean_variable_2);
                //lexemes_table.Rows.Add(boolean_variable_2, "Variable Identifier");

                if (boolean_keyword != "") lexemes_table.Rows.Add(boolean_keyword, "Boolean Keyword");

                checkExpression(boolean_variable_3);
                //lexemes_table.Rows.Add(boolean_variable_3, "Variable Identifier");
                if (BOOLEAN_OPERATION.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = BOOLEAN_OPERATION.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);

                }

            }
        }

        private void isBooleanOperation2(String str)
        {
            if (BOOLEAN_OPERATION_2.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = BOOLEAN_OPERATION_2.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            String boolean_not = BOOLEAN_OPERATION_2.Match(str).Groups["boolean_not"].Value.ToString();
            String boolean_variable = BOOLEAN_OPERATION_2.Match(str).Groups["boolean_variable"].Value.ToString();

            lexemes_table.Rows.Add(boolean_not, "Boolean Operation");
            checkExpression(boolean_variable);

            if (BOOLEAN_OPERATION_2.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = BOOLEAN_OPERATION_2.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);

            }
        }

        // this method is for comparison operation checking and putting it to the lexemes table
        private void isComparisonOperation(String str)
        {
            if (COMPARISON_1.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = COMPARISON_1.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            if (COMPARISON_2.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = COMPARISON_2.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            // (BOTH SAEM|DIFFRINT) <x> [AN] <y>
            if (COMPARISON_1.Match(str).Success)
            {
                String comparison_keyword = COMPARISON_1.Match(str).Groups["comparison_keyword"].Value.ToString();
                String comparison_expression_1 = COMPARISON_1.Match(str).Groups["comparison_expression_1"].Value.ToString();
                String comparison_noise_word = COMPARISON_1.Match(str).Groups["comparison_noise_word"].Value.ToString();
                String comparison_expression_2 = COMPARISON_1.Match(str).Groups["comparison_expression_2"].Value.ToString();

                lexemes_table.Rows.Add(comparison_keyword, "Comparison Keyword");
                checkExpression(comparison_expression_1);
                if (!String.IsNullOrEmpty(comparison_noise_word))
                {
                    lexemes_table.Rows.Add(comparison_noise_word, "Comparison Noise Word");
                }
                checkExpression(comparison_expression_2);

                if (COMPARISON_1.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = COMPARISON_1.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);
                }
            }

            else
            {
                String comparison_keyword_1 = COMPARISON_2.Match(str).Groups["comparison_keyword_1"].Value.ToString();
                String comparison_expression_1 = COMPARISON_2.Match(str).Groups["comparison_expression_1"].Value.ToString();
                String comparison_keyword_2 = COMPARISON_2.Match(str).Groups["comparison_keyword_2"].Value.ToString();
                String comparison_expression_2 = COMPARISON_2.Match(str).Groups["comparison_expression_2"].Value.ToString();
                String coparison_noise_word = COMPARISON_2.Match(str).Groups["comparison_noise_word"].Value.ToString();
                String comparison_expression_3 = COMPARISON_2.Match(str).Groups["comparison_expression_3"].Value.ToString();

                lexemes_table.Rows.Add(comparison_keyword_1, "Comparaison Operation Keyword");
                checkExpression(comparison_expression_1);
                lexemes_table.Rows.Add(comparison_keyword_2, "Comparison Operation Keyword");
                checkExpression(comparison_expression_2);
                if (!String.IsNullOrEmpty(coparison_noise_word))
                {
                    lexemes_table.Rows.Add(coparison_noise_word, "Corapsiron Operation Noise word");
                }
                checkExpression(comparison_expression_3);

                if (COMPARISON_2.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = COMPARISON_2.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);
                }
            }
        }

        // this method is for type casting checking and putting it to the lexemes table
        private void isTypeCasting(String str)
        {
            // MAEK <expression> [A] <type>
            if (TYPECAST.Match(str).Success)
            {
                String typecasting_keyword_1 = TYPECAST.Match(str).Groups["typecasting_keyword_1"].Value.ToString();
                String variable_expression = TYPECAST.Match(str).Groups["typecasting_expression"].Value.ToString();
                String typecasting_keyword_2 = TYPECAST.Match(str).Groups["typecasting_keyword_2"].Value.ToString();
                String variable_type = TYPECAST.Match(str).Groups["variable_type"].Value.ToString();

                lexemes_table.Rows.Add(typecasting_keyword_1, "Casting Keyword");
                checkExpression(variable_expression);
                if (!String.IsNullOrEmpty(typecasting_keyword_2))
                {
                    lexemes_table.Rows.Add(typecasting_keyword_2, "Casting Keyword");
                }
                lexemes_table.Rows.Add(variable_type, "Variable Type");

                if (TYPECAST.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = TYPECAST.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);

                }

            }

            // <variable> IS NOW A <type>
            else if (RECAST_1.Match(str).Success)
            {
                String variable_name = RECAST_1.Match(str).Groups["variable_name"].Value.ToString();
                String recasting_keyword = RECAST_1.Match(str).Groups["recasting_keyword"].Value.ToString();
                String variable_type = RECAST_1.Match(str).Groups["variable_type"].Value.ToString();

                lexemes_table.Rows.Add(variable_name, "Variable Identifier");
                lexemes_table.Rows.Add(recasting_keyword, "Casting Keyword");
                lexemes_table.Rows.Add(variable_type, "Variable Type");

                if (RECAST_1.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = RECAST_1.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);
                }

            }

            // <variable> R MAEK <variable> [A] <type>
            else if (RECAST_2.Match(str).Success)
            {
                String variable_name_1 = RECAST_2.Match(str).Groups["variable_name_1"].Value.ToString();
                String recasting_keyword = RECAST_2.Match(str).Groups["recasting_keyword"].Value.ToString();
                String variable_name_2 = RECAST_2.Match(str).Groups["variable_name_2"].Value.ToString();
                String recasting_noise_word = RECAST_2.Match(str).Groups["recasting_noise_word"].Value.ToString();
                String variable_type = RECAST_2.Match(str).Groups["variable_type"].Value.ToString();

                lexemes_table.Rows.Add(variable_name_1, "Variable Identifier");
                lexemes_table.Rows.Add(recasting_keyword, "Casting Keyword");
                lexemes_table.Rows.Add(variable_name_2, "Variable Identifier");
                if (!String.IsNullOrEmpty(recasting_noise_word))
                {
                    lexemes_table.Rows.Add(recasting_noise_word, "Casting Noise Word");
                }
                lexemes_table.Rows.Add(variable_type, "Variable Type");

                if (RECAST_2.Match(str).Groups["comment_line"].Value != "")
                {
                    String comment = RECAST_2.Match(str).Groups["comment_line"].Value.ToString();
                    checkExpression(comment);
                }

            }
        }

        // this method is for concatenations checking and putting it to the lexemes table
        private void isConcatenation(String str)
        {
            String concatenation_keyword_1 = CONCATENATION.Match(str).Groups["concatenation_keyword_1"].Value.ToString();
            String concatenation_expression_1 = CONCATENATION.Match(str).Groups["concatenation_expression_1"].Value.ToString();
            String concatenation_noise_word = CONCATENATION.Match(str).Groups["concatenation_noise_word"].Value.ToString();
            String concatenation_expression_2 = CONCATENATION.Match(str).Groups["concatenation_expression_2"].Value.ToString();
            String concatenation_keyword_2 = CONCATENATION.Match(str).Groups["concatenation_keyword_2"].Value.ToString();

            lexemes_table.Rows.Add(concatenation_keyword_1, "Concatenation Keyword");
            checkExpression(concatenation_expression_1);
            if (!String.IsNullOrEmpty(concatenation_noise_word)) lexemes_table.Rows.Add(concatenation_noise_word, "Concatenation Noise Word");
            checkExpression(concatenation_expression_2);
            if (!String.IsNullOrEmpty(concatenation_keyword_2)) lexemes_table.Rows.Add(concatenation_keyword_2, "Concatenation Keyword");


            if (CONCATENATION.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = CONCATENATION.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }

        }

        // this method is for addition operation checking and putting it to the lexemes table
        private void isAddition(String str)
        {
            if (ADDITION.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = ADDITION.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            String addition_keyword = ADDITION.Match(str).Groups["addition_keyword"].Value.ToString();
            String operand_1 = ADDITION.Match(str).Groups["operand_1"].Value.ToString();
            String separator = ADDITION.Match(str).Groups["separator"].Value.ToString();
            String operand_2 = ADDITION.Match(str).Groups["operand_2"].Value.ToString();

            lexemes_table.Rows.Add(addition_keyword, "Addition Keyword");
            checkExpression(operand_1);
            lexemes_table.Rows.Add(separator, "Addition Noise Word");
            checkExpression(operand_2);


            if (ADDITION.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = ADDITION.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for subtraction operation checking and putting it to the lexemes table
        private void isSubtraction(String str)
        {
            if (SUBTRACTION.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = SUBTRACTION.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            String subtraction_keyword = SUBTRACTION.Match(str).Groups["subtraction_keyword"].Value.ToString();
            String operand_1 = SUBTRACTION.Match(str).Groups["operand_1"].Value.ToString();
            String separator = SUBTRACTION.Match(str).Groups["separator"].Value.ToString();
            String operand_2 = SUBTRACTION.Match(str).Groups["operand_2"].Value.ToString();

            lexemes_table.Rows.Add(subtraction_keyword, "Subtraction Keyword");
            checkExpression(operand_1);
            lexemes_table.Rows.Add(separator, "Separator Keyword");
            checkExpression(operand_2);

            if (SUBTRACTION.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = SUBTRACTION.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for multiplication operation checking and putting it to the lexemes table
        private void isMultiplication(String str)
        {
            if (MULTIPLICATION.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = MULTIPLICATION.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            String multiplication_keyword = MULTIPLICATION.Match(str).Groups["multiplication_keyword"].Value.ToString();
            String operand_1 = MULTIPLICATION.Match(str).Groups["operand_1"].Value.ToString();
            String separator = MULTIPLICATION.Match(str).Groups["separator"].Value.ToString();
            String operand_2 = MULTIPLICATION.Match(str).Groups["operand_2"].Value.ToString();

            lexemes_table.Rows.Add(multiplication_keyword, "Multiplication Keyword");
            checkExpression(operand_1);
            lexemes_table.Rows.Add(separator, "Separator Keyword");
            checkExpression(operand_2);

            if (MULTIPLICATION.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = MULTIPLICATION.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for division operation checking and putting it to the lexemes table
        private void isDivision(String str)
        {
            if (QUOSHUNT.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = QUOSHUNT.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            String division_keyword = QUOSHUNT.Match(str).Groups["division_keyword"].Value.ToString();
            String operand_1 = QUOSHUNT.Match(str).Groups["operand_1"].Value.ToString();
            String separator = QUOSHUNT.Match(str).Groups["separator"].Value.ToString();
            String operand_2 = QUOSHUNT.Match(str).Groups["operand_2"].Value.ToString();

            lexemes_table.Rows.Add(division_keyword, "Division Keyword");
            checkExpression(operand_1);
            lexemes_table.Rows.Add(separator, "Separator Keyword");
            checkExpression(operand_2);

            if (QUOSHUNT.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = QUOSHUNT.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for modulo operation checking and putting it to the lexemes table
        private void isModulo(String str)
        {
            if (MODULO.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = MODULO.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            String modulo_keyword = MODULO.Match(str).Groups["modulo_keyword"].Value.ToString();
            String operand_1 = MODULO.Match(str).Groups["operand_1"].Value.ToString();
            String separator = MODULO.Match(str).Groups["separator"].Value.ToString();
            String operand_2 = MODULO.Match(str).Groups["operand_2"].Value.ToString();

            lexemes_table.Rows.Add(modulo_keyword, "Modulo Keyword");
            checkExpression(operand_1);
            lexemes_table.Rows.Add(separator, "Separator Keyword");
            checkExpression(operand_2);

            if (MODULO.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = MODULO.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for bigger operation checking and putting it to the lexemes table
        private void isBigger(String str)
        {
            if (BIGGR.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = BIGGR.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            String bigger_keyword = BIGGR.Match(str).Groups["bigger_keyword"].Value.ToString();
            String operand_1 = BIGGR.Match(str).Groups["operand_1"].Value.ToString();
            String separator = BIGGR.Match(str).Groups["separator"].Value.ToString();
            String operand_2 = BIGGR.Match(str).Groups["operand_2"].Value.ToString();

            lexemes_table.Rows.Add(bigger_keyword, "Bigger Keyword");
            checkExpression(operand_1);
            lexemes_table.Rows.Add(separator, "Separator Keyword");
            checkExpression(operand_2);

            if (BIGGR.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = BIGGR.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for smaller operation checking and putting it to the lexemes table
        private void isSmaller(String str)
        {
            if (SMALLR.Match(str).Groups["mebbe_keyword"].Value != "")
            {
                String mebbe_keyword = SMALLR.Match(str).Groups["mebbe_keyword"].Value.ToString();
                lexemes_table.Rows.Add(mebbe_keyword, "If-then keyword");
            }

            String smaller_keyword = SMALLR.Match(str).Groups["smaller_keyword"].Value.ToString();
            String operand_1 = SMALLR.Match(str).Groups["operand_1"].Value.ToString();
            String separator = SMALLR.Match(str).Groups["separator"].Value.ToString();
            String operand_2 = SMALLR.Match(str).Groups["operand_2"].Value.ToString();

            lexemes_table.Rows.Add(smaller_keyword, "Smaller Keyword");
            checkExpression(operand_1);
            lexemes_table.Rows.Add(separator, "Separator Keyword");
            checkExpression(operand_2);

            if (SMALLR.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = SMALLR.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for visible checking and putting it to the lexemes table
        private void isVisible(String str)
        {
            String basic_output_keyword = VISIBLE.Match(str).Groups["basic_output_keyword"].Value.ToString();
            String expression = VISIBLE.Match(str).Groups["expression"].Value.ToString();

            lexemes_table.Rows.Add(basic_output_keyword, "Basic Output Keyword");
            checkExpression(expression);

            if (VISIBLE.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = VISIBLE.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);
            }
        }

        // this method is for if-then conditional checking and putting it to the lexemes table
        private void isIfThenKeyword(String str)
        {
            String ifthen_keywords = IFTHEN_KEYWORDS.Match(str).Groups["ifthen_keywords"].Value.ToString();
            String expression = SMALLR.Match(str).Groups["expression"].Value.ToString();

            lexemes_table.Rows.Add(ifthen_keywords, "If-Then Keyword");

            if (IFTHEN_KEYWORDS.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = IFTHEN_KEYWORDS.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);

            }

        }

        // this method is for case conditional checking and putting it to the lexemes table
        private void isCaseConditionalKeyword(String str)
        {
            if (CASE_CONDITIONAL_KEYWORD.Match(str).Groups["var_ident"].Value != "")
            {
                String var_ident = CASE_CONDITIONAL_KEYWORD.Match(str).Groups["var_ident"].Value.ToString();
                lexemes_table.Rows.Add(var_ident, "Variable Identifier");
            }

            String case_keyword = CASE_CONDITIONAL_KEYWORD.Match(str).Groups["case_keyword"].Value.ToString();

            lexemes_table.Rows.Add(case_keyword, "Case Conditional Keyword");

            if (CASE_CONDITIONAL_KEYWORD.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = CASE_CONDITIONAL_KEYWORD.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);

            }

        }

        // this method is for case conditional case checking and putting it to the lexemes table
        private void isCaseConditionalCase(String str)
        {
            String case_keyword = CASE_CONDITIONAL_CASES.Match(str).Groups["case_keyword"].Value.ToString();
            String literal = CASE_CONDITIONAL_CASES.Match(str).Groups["literal"].Value.ToString();

            lexemes_table.Rows.Add(case_keyword, "Case Conditional Keyword");
            lexemes_table.Rows.Add(literal, "Case Literal");

            if (CASE_CONDITIONAL_CASES.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = CASE_CONDITIONAL_CASES.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);

            }

        }

        // this method is for program delimeter checking and putting it to the lexemes table
        private void isProgramDelimiter(String str)
        {

            String program_delimiter = PROGRAM_DELIMITER.Match(str).Groups["program_delimiter"].Value.ToString();

            lexemes_table.Rows.Add(program_delimiter, "Program Delimiter");

            if (PROGRAM_DELIMITER.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = PROGRAM_DELIMITER.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);

            }



        }

        // this method is for OIC checking and putting it to the lexemes table
        private void isTerminator(String str)
        {
            String terminator = TERMINATOR.Match(str).Groups["terminator_keyword"].Value.ToString();

            lexemes_table.Rows.Add(terminator, "Conditional Terminator");

            if (TERMINATOR.Match(str).Groups["comment_line"].Value != "")
            {
                String comment = TERMINATOR.Match(str).Groups["comment_line"].Value.ToString();
                checkExpression(comment);

            }

        }

        private void isMultilineComment(String str)
        {
            String multi_comment_kw = MULTILINE_COMMENT.Match(str).Groups["multi_comment"].Value.ToString();

            lexemes_table.Rows.Add(multi_comment_kw, "Multiline Comment Delimiter");

            if (multi_comment_kw.CompareTo("OBTW") == 0)
            {
                multi_comment = true;
            }
            else
            {
                multi_comment = false;
            }
        }

        private void isFunction(String str)
        {

            if (FUNCTION_1.Match(str).Success)
            {
                String function_delimiter = FUNCTION_1.Match(str).Groups["function_delimiter"].Value.ToString();
                String function_name = FUNCTION_1.Match(str).Groups["function_name"].Value.ToString();

                lexemes_table.Rows.Add(function_delimiter, "Function Delimiter");
                lexemes_table.Rows.Add(function_name, "Function Label");

                if (FUNCTION_1.Match(str).Groups["function_keyword_1"].Value != "")
                {
                    lexemes_table.Rows.Add(FUNCTION_1.Match(str).Groups["function_keyword_1"].Value, "Function Keyword");
                }
                if (FUNCTION_1.Match(str).Groups["argument_1"].Value != "")
                {
                    lexemes_table.Rows.Add(FUNCTION_1.Match(str).Groups["argument_1"].Value, "Function Argument");
                }
                if (FUNCTION_1.Match(str).Groups["function_noise_word"].Value != "")
                {
                    lexemes_table.Rows.Add(FUNCTION_1.Match(str).Groups["function_noise_word"].Value, "Function Noise Word");
                }
                if (FUNCTION_1.Match(str).Groups["function_keyword_2"].Value != "")
                {
                    lexemes_table.Rows.Add(FUNCTION_1.Match(str).Groups["function_keyword_2"].Value, "Function Keyword");
                }
                if (FUNCTION_1.Match(str).Groups["argument_2"].Value != "")
                {
                    lexemes_table.Rows.Add(FUNCTION_1.Match(str).Groups["argument_2"].Value, "Function Argument");
                }
            }
            else if (FUNCTION_2.Match(str).Success)
            {
                String function_delimiter = FUNCTION_2.Match(str).Groups["function_delimiter"].Value.ToString();
                lexemes_table.Rows.Add(function_delimiter, "Function Delimiter");
            }
        }

        //this method if for checking what kind of expression is used
        private void checkExpression(String str)
        {
            if (ADDITION.Match(str).Success)
            {
                isAddition(str);
            }

            else if (SUBTRACTION.Match(str).Success)
            {
                isSubtraction(str);
            }

            else if (MULTIPLICATION.Match(str).Success)
            {
                isMultiplication(str);
            }

            else if (QUOSHUNT.Match(str).Success)
            {
                isDivision(str);
            }

            else if (MODULO.Match(str).Success)
            {
                isModulo(str);
            }

            else if (BIGGR.Match(str).Success)
            {
                isBigger(str);
            }

            else if (SMALLR.Match(str).Success)
            {
                isSmaller(str);
            }

            else if (COMPARISON_1.Match(str).Success || COMPARISON_2.Match(str).Success)
            {
                isComparisonOperation(str);
            }

            else if (BOOLEAN_OPERATION.Match(str).Success)
            {
                isBooleanOperation(str);
            }

            else if (BOOLEAN_OPERATION_2.Match(str).Success)
            {
                isBooleanOperation2(str);
            }

            else if (COMMENT.Match(str).Success)
            {
                isComment(str);
            }


            else if (CONCATENATION.Match(str).Success)
            {
                isConcatenation(str);
            }

            else if (TROOF.Match(str).Success)
            {
                lexemes_table.Rows.Add(str, "Boolean Literal");
            }

            else if (YARN.Match(str).Success && System.Text.RegularExpressions.Regex.IsMatch(str, @"^""[\w\W]*""$", RegexOptions.None))
            {
                lexemes_table.Rows.Add(str, "Character String");
            }

            else if (NUMBAR.Match(str).Success && !str.Contains(" "))
            {
                lexemes_table.Rows.Add(str, "Number Literal");
            }

            else if (NUMBR.Match(str).Success && !str.Contains(" "))
            {
                lexemes_table.Rows.Add(str, "Number Literal");
            }

            else if (VAR_NAME.Match(str).Success && !str.Contains(" "))
            {
                lexemes_table.Rows.Add(str, "Variable Identifier");
            }


            // checks for multiple expression in visible
            else if (System.Text.RegularExpressions.Regex.IsMatch(str, @"\s+|""[\w\s]*""", RegexOptions.None))
            {

                String[] word;
                Boolean is_a_string = false;
                String str_accumulator = "";

                if (str.Contains('"'))
                {
                    word = str.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String line in word)
                    {
                        if (line.StartsWith("\""))
                        {
                            str_accumulator = line + " ";
                            is_a_string = true;
                            continue;
                        }

                        if (is_a_string)
                        {
                            if (line.EndsWith("\""))
                            {
                                str_accumulator += line;
                                is_a_string = false;
                                checkExpression(str_accumulator);
                            }

                            else
                            {
                                str_accumulator += line + " ";
                            }
                        }

                        else
                        {
                            checkExpression(line);
                        }
                    }
                }

                else
                {
                    word = str.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String line in word)
                    {
                        checkExpression(line);
                    }
                }

            }
        }

        public void printToConsole(String text)
        {
            String previous_content = output_console.Text;
            output_console.Text = previous_content + text + Environment.NewLine;
        }


        // this method shows all detected errors in the lexical analysis and interpretation
        public void promptError(int error_code, String variable_name, int line_index)
        {
            switch (error_code)
            {
                // lexical analysis error, no/incomplete program delimeter
                case 0:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Source code must start with a HAI and end with a KTHXBYE." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // lexical analysis error, syntax error
                case 1:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Line " + line_index.ToString() + ", an error in the syntax was detected." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // variable declaration error, variable already exists
                case 2:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: The variable " + variable_name + " already exists." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // basic output error, variable with type NOOB
                case 3:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Variable with type NOOB cannot be implicity typecasted into String." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // basic output error, variable does does not exists
                case 4:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: The variable " + variable_name + " does not exist." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // arithmetic operation error, cannot execute operation in yarn
                case 5:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute mathemetical operation because YARN variable cannot be typecasted into NUMBAR or NUMBR." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // arithmetic operation error, variable does not exists
                case 6:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute mathemetical operation because operand does not exists." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // arithmetic operation error, variable type is noob
                case 7:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute mathematical operation because NOOB cannot be casted into NUMBR/NUMBAR" + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // assignment operation error, variable does not exits
                case 8:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute assignment operation. The variable to be assigned does not exists." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // conditional statement error, missing ya rly
                case 9:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute conditional statemet. Missing YA RLY clause." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // conditional statement error, missing no wai
                case 10:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute conditional statement. Missing NO WAI clause" + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // conditional statement error, missing oic
                case 11:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute conditional statement. Missing OIC clause" + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // switch statement error, switch control statement is not a yarn
                case 12:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute switch statement. The variable store in IT is not of type YARN." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // switch statement error, missing OMG
                case 13:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute switch statement. Missing OMG statement." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // switch statement error, missing OIC
                case 14:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute switch statement. Missing OIC statement." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // comparison operation error, cannot cast YARN to NUMBR/NUMBAR
                case 15:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute comparison operation. Cannot cast YARN to NUMBR/NUMBAR." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // comparison operation error, variable operand does not exist
                case 16:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute comparison operation. Variable operand does not exist." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;


                // comparison operation error, variable type is noob
                case 17:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute comparison operation. Cannot cast NOOB to NUMBR/NUMBAR." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // boolean opearation error, variable does not exist
                case 18:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute boolean operation. Variable does not exist." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // boolean opearation error, variable is not troof
                case 19:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute boolean operation. Cannot cast variable type to TROOF." + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                // loop opearation error, missing im outta yr clause
                case 20:
                    output_console.Text += Environment.NewLine + "==========================================================================================" + Environment.NewLine + "Error: Cannot execute loop operation. Missing IM OUTTA YR clause" + Environment.NewLine + "==========================================================================================" + Environment.NewLine;
                    break;

                default:
                    break;
            }
        }

        public void updateSymbolTable(String variable_name, String variable_type, String variable_value)
        {
            symbol_table.Rows.Add(variable_name, variable_type, variable_value);
        }

        public void clearSymboltable()
        {
            symbol_table.Rows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input_console.Clear();
            lexemes_table.Rows.Clear();
            symbol_table.Rows.Clear();
            output_console.Clear();
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void input_console_SelectionChanged(object sender, EventArgs e)
        {
            int selectionStart = input_console.SelectionStart;
            int lineIndex = input_console.GetLineFromCharIndex(selectionStart);
            lineIndex = lineIndex + 1;
            LineNumber.Text = "Line: " + lineIndex.ToString();
        }

        private void pROJECTDETAILSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.ShowDialog();
        }

        private void dEVELOPERSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.ShowDialog();
        }

    }
}

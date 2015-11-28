using System;
using Gtk;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data;
using System.Drawing;
using Microsoft.VisualBasic;

public partial class MainWindow: Gtk.Window
{	
    Gtk.ListStore LexemeStore = new Gtk.ListStore (typeof (string), typeof (string));
	Gtk.ListStore SymbolTableStore = new Gtk.ListStore (typeof (string), typeof (string), typeof (string));

	Dictionary<string, int> variables = new Dictionary<string, int>();
	Dictionary<string, string> vartype = new Dictionary<string, string>();

	static Regex VAR_NAME = new Regex(@"[A-Za-z][A-Za-z0-9_]*", RegexOptions.None);
	static Regex NUMBR = new Regex(@"(-)?[0-9]+", RegexOptions.None);
	static Regex NUMBAR = new Regex(@"(-)?[0-9]+\.[0-9]+", RegexOptions.None);
	static Regex TROOF = new Regex(@"(WIN|FAIL)", RegexOptions.None);
	static Regex YARN = new Regex(@".+", RegexOptions.None);
	static Regex STRING = new Regex("\".*\"", RegexOptions.None);

	static Regex HAI = new Regex(@"^(HAI)\s*", RegexOptions.None);
	static Regex KTHXBYE = new Regex(@"^\s*(KTHXBYE)\s*", RegexOptions.None);

	static Regex EXPRESSION = new Regex(@"(" + VAR_NAME + "|" + NUMBR + "|" + NUMBAR + "|" + TROOF + "|" + YARN + @")", RegexOptions.None);
	static Regex COMPARISION_EXPRESSION = new Regex(@"(" + TROOF + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + "|" + VAR_NAME + "|" + NUMBAR + "|" + NUMBR + "|" + YARN + ")", RegexOptions.None);
	static Regex COMPARISON_1 = new Regex(@"(<comparison_keyword>(BOTH\s+SAEM|DIFFRINT))\s+(?<comparison_expression_1>" + COMPARISION_EXPRESSION + @")\s+((?<comparison_noise_word>AN)\s+)?(?<comparison_expression_2>" + COMPARISION_EXPRESSION + ")$", RegexOptions.None);
	static Regex COMPARISON_2 = new Regex(@"((?<mebbe_keyword>MEBBE)\s+)?(?<comparison_keyword_1>(BOTH\s+SAEM|DIFFRINT))\s+(?<comparison_expression_1>" + COMPARISION_EXPRESSION + @")\s+(?<comparison_keyword_2>(AND\s+BIGGR\s+OF|AND\s+SMALLR\s+OF))\s+(?<comparison_expression_2>" + COMPARISION_EXPRESSION + @")\s+(?<comparison_noise_word>AN)\s+(?<comparison_expression_3>" + COMPARISION_EXPRESSION + ")$", RegexOptions.None);
	static Regex SMOOSH = new Regex(@"(?<keyword>SMOOSH)\s+(?<part1>" + EXPRESSION + @")\s+(?<and>AN\s+)?(?<part2>" + EXPRESSION + @")\s+(?<keyword2>MKAY)", RegexOptions.None);

	static Regex I_HAS_A = new Regex(@"(?<vardec>I\s+HAS\s+A)\s+(?<identifier>" + VAR_NAME + @")\s*$", RegexOptions.None);
	static Regex ITZ = new Regex(@"(?<vardec>I\s+HAS\s+A)\s+(?<identifier>" + VAR_NAME + @")\s+(?<itz>ITZ)\s+(?<variable_type>(" + NUMBAR + "|" + NUMBR + "|" + YARN + "|" + TROOF + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + "|" + MOD_OF + "|" + SMOOSH + "|" + COMPARISON_1 + "|" + COMPARISON_2 + @"))", RegexOptions.None);

	static Regex BTW = new Regex(@"(?<comment>BTW).*", RegexOptions.None);
	static Regex OBTW = new Regex(@"(?<comment1>OBTW)\s*.*(?<comment2>TLDR)?\s*", RegexOptions.None);
	static Regex TLDR = new Regex(@"\s*(?<comment>TLDR)\s*", RegexOptions.None);
	static Regex R = new Regex(@"(?<vardec>R)\s+(?<identifier>" + YARN + @")\s*", RegexOptions.None);

	static Regex SUM_OF_EXPRESSION = new Regex(@"(?<keyword>SUM\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))$");
	static Regex SUM_OF = new Regex(@"(?<keyword>SUM\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))");
	static Regex DIFF_OF_EXPRESSION = new Regex(@"(?<keyword>DIFF\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))");
	static Regex DIFF_OF = new Regex(@"(?<keyword>DIFF\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))");
	static Regex PRODUKT_OF_EXPRESSION = new Regex(@"(?<keyword>PRODUKT\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))");
	static Regex PRODUKT_OF = new Regex(@"(?<keyword>PRODUKT\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))");
	static Regex QUOSHUNT_OF_EXPRESSION = new Regex(@"(?<keyword>QUOSHUNT\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))");
	static Regex QUOSHUNT_OF = new Regex(@"(?<keyword>QUOSHUNT\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))");
	static Regex MOD_OF_EXPRESSION = new Regex(@"(?<keyword>MOD\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))");
	static Regex MOD_OF = new Regex(@"(?<keyword>MOD\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))");
	static Regex BIGGR_OF_EXPRESSION = new Regex(@"(?<keyword>BIGGR\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))");
	static Regex BIGGR_OF = new Regex(@"(?<keyword>BIGGR\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))");
	static Regex SMALLR_OF_EXPRESSION = new Regex(@"(?<keyword>SMALLR\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF + "|" + DIFF_OF + "|" + PRODUKT_OF + "|" + QUOSHUNT_OF + "|" + MOD_OF + "|" + BIGGR_OF + "|" + SMALLR_OF + @"))");
	static Regex SMALLR_OF = new Regex(@"(?<keyword>SMALLR\s+OF)\s+(?<operand_1>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))\s+(?<and>AN)\s+(?<operand_2>(" + EXPRESSION + "|" + SUM_OF_EXPRESSION + "|" + DIFF_OF_EXPRESSION + "|" + PRODUKT_OF_EXPRESSION + "|" + QUOSHUNT_OF_EXPRESSION + "|" + MOD_OF_EXPRESSION + "|" + BIGGR_OF_EXPRESSION + "|" + SMALLR_OF_EXPRESSION + @"))");

	static Regex BOTH_OF = new Regex(@"(?<keyword>BOTH\sOF)\s+(?<operand_1>" + EXPRESSION + @")\s+(?<and>AN)\s+(?<operand_2>" + EXPRESSION + @")\s*", RegexOptions.None);
	static Regex EITHER_OF = new Regex(@"(?<keyword>EITHER\sOF)\s+(?<operand_1>" + EXPRESSION + @")\s+(?<and>AN)\s+(?<operand_2>" + EXPRESSION + @")\s*", RegexOptions.None);
	static Regex WON_OF = new Regex(@"(?<keyword>WON\sOF)\s+(?<operand_1>" + EXPRESSION + @")\s+(?<and>AN)\s+(?<operand_2>" + EXPRESSION + @")\s*", RegexOptions.None);
	static Regex NOT = new Regex(@"(?<keyword>NOT)\s+(?<operand>" + EXPRESSION + @")\s*", RegexOptions.None);
	static Regex ALL_OF = new Regex(@"(?<keyword>ALL\sOF)\s+(?<operand_1>" + EXPRESSION + @")\s+(?<and>AN)\s+(?<operand_2>" + EXPRESSION + @")\s*", RegexOptions.None);
	static Regex ANY_OF = new Regex(@"(?<keyword>ANY\sOF)\s+(?<operand_1>" + EXPRESSION + @")\s+(?<and>AN)\s+(?<operand_2>" + EXPRESSION + @")\s*", RegexOptions.None);

	static Regex BOTH_SAEM = new Regex(@"(?<keyword>BOTH\sSAEM)\s+(?<part1>" + YARN + @")\s+(?<and>AN)\s+(?<part2>" + YARN + @")\s*", RegexOptions.None);
	static Regex DIFFRINT = new Regex(@"(?<keyword>DIFFRINT)\s+(?<part1>" + YARN + @")\s+(?<and>AN)\s+(?<part2>" + YARN + @")\s*", RegexOptions.None);

	static Regex MAEK = new Regex(@"(?<keyword>MAEK)\s+(?<part1>" + YARN + @")\s+(?<and>A)\s+(?<part2>" + YARN + @")\s*", RegexOptions.None);
	static Regex IS_NOW_A = new Regex(@"(?<part1>" + YARN + @")\s+(?<keyword>IS\sNOW\sA)\s+(?<part2>" + YARN + @")\s*", RegexOptions.None);

	static Regex VISIBLE = new Regex(@"(?<keyword>VISIBLE)\s+(?<identifier>" + STRING + @")\s*", RegexOptions.None);
	static Regex VISIBLE2 = new Regex(@"(?<keyword>VISIBLE)\s+(?<identifier>" + VAR_NAME + @")\s*", RegexOptions.None);
	
	static Regex GIMMEH = new Regex(@"(?<keyword>GIMMEH)\s+(?<identifier>" + YARN + @")\s*", RegexOptions.None);

	static Regex ORLY = new Regex(@"(O)\s(RLY)\s*",RegexOptions.Multiline);
	static Regex MEBBE = new Regex(@"(MEBBE)\s(?<expression>" + EXPRESSION + @")\s*",RegexOptions.None);
	static Regex NOWAI = new Regex(@"(NO)\s(WAI)\s*",RegexOptions.None);
	static Regex YARLY = new Regex(@"(YA)\s(RLY)\s*",RegexOptions.None);
	static Regex OIC = new Regex(@"(OIC)\s*",RegexOptions.None);
	static Regex WTF = new Regex(@"(WTF)\s*",RegexOptions.None);
	static Regex OMG = new Regex(@"(OMG)\s(?<identifier>" + NUMBR + @")\s*",RegexOptions.None);
	static Regex OMGWTF = new Regex(@"(OMGWTF)\s*",RegexOptions.None);
	static Regex IMINYR = new Regex(@"(IM)\s(IN)\s(YR)\s(?<label>" + VAR_NAME + @")\s(?<operation>((UPPIN)|(NERFIN)))\s(?<your>(YR))\s(?<variable>" + VAR_NAME + @")\s(?<eval>((TIL)|(WILE)))\s(?<expression>" + EXPRESSION + @")\s*",RegexOptions.None);
	static Regex IMOUTTAYR = new Regex(@"(IM)\s(OUTTA)\s(YR)\s(?<identifier>" + VAR_NAME + @")\s*",RegexOptions.None);

	static Regex RESERVED = new Regex(@"\s*(HAI|KTHXBYE|WIN|FAIL|AN|SMOOSH|MKAY|ITZ|BTW|OBTW|TLDR|MAEK|VISIBLE|GIMMEH|MEBBE|YARLY|OIC|WTF|OMG|OMGWTF|UPPIN|NERFIN)\s*",RegexOptions.None);

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		LexTreeViewInit();
		SymbolTableInit ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnAddFileActionActivated (object sender, EventArgs e)
	{
		 using(FileChooserDialog chooser = new FileChooserDialog (null,"Open File", null, FileChooserAction.Open,"Cancel", ResponseType.Cancel, "Open", ResponseType.Accept)) {
            if (chooser.Run () == (int)ResponseType.Accept) {
	            System.IO.StreamReader file = System.IO.File.OpenText (chooser.Filename);
	            TextEditor.Buffer.Text = "  " + file.ReadToEnd ();
	            file.Close ();
				chooser.Destroy();
			}
     	}
	}

	public void LexTreeViewInit ()
	{
		Gtk.TreeViewColumn LexemeColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn ClassColumn = new Gtk.TreeViewColumn ();
    	LexemeColumn.Title = "Lexeme";
   		ClassColumn.Title = "Classification";

	    LexTree.AppendColumn (LexemeColumn);
		LexTree.AppendColumn (ClassColumn);

		LexTree.Model = LexemeStore;
		Gtk.CellRendererText LexCell = new Gtk.CellRendererText ();
		LexemeColumn.PackStart (LexCell, true);
		Gtk.CellRendererText ClassCell = new Gtk.CellRendererText ();
		ClassColumn.PackStart (ClassCell, true);

		LexemeColumn.AddAttribute (LexCell, "text", 0);
		ClassColumn.AddAttribute (ClassCell, "text", 1);
	}	

	public void SymbolTableInit ()
	{
		Gtk.TreeViewColumn VariableColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn TypeColumn = new Gtk.TreeViewColumn ();
		Gtk.TreeViewColumn ValueColumn = new Gtk.TreeViewColumn ();
		VariableColumn.Title = "Variable";
		TypeColumn.Title = "Type";
		ValueColumn.Title = "Value";

		SymbolTable.AppendColumn (VariableColumn);
		SymbolTable.AppendColumn (TypeColumn);
		SymbolTable.AppendColumn (ValueColumn);

		SymbolTable.Model = SymbolTableStore;
		Gtk.CellRendererText VariableCell = new Gtk.CellRendererText ();
		VariableColumn.PackStart (VariableCell, true);
		Gtk.CellRendererText TypeCell = new Gtk.CellRendererText ();
		TypeColumn.PackStart (TypeCell, true);
		Gtk.CellRendererText ValueCell = new Gtk.CellRendererText ();
		ValueColumn.PackStart (ValueCell, true);

		VariableColumn.AddAttribute (VariableCell, "text", 0);
		TypeColumn.AddAttribute (TypeCell, "text", 1);
		ValueColumn.AddAttribute (ValueCell, "text", 2);
	}	

	protected void OnExecuteBtnClicked (object sender, EventArgs e)
	{
		LexemeStore.Clear();
		SymbolTableStore.Clear();
		Console.Buffer.Text = "";

		string input = TextEditor.Buffer.Text;
		String[] code_lines = input.Split(new String[] { "\n" }, StringSplitOptions.None);
		code_lines = (from line in code_lines select line.Trim()).ToArray();

		int long_comment = 0;
		int err = 0;
		int i = 0;

		foreach (String line in code_lines)
		{
			int lineIndex = Array.IndexOf(code_lines, line) + 1;

			if (code_lines[0].CompareTo("HAI") != 0 || code_lines[code_lines.Length - 1].CompareTo("KTHXBYE") != 0) {
				Console.Buffer.Text = "";
				printError(0, 0, null);
				break;
			}

			if (HAI.Match (line).Success) {
				LexemeStore.AppendValues ("HAI", "Code Delimiter");
			} else if (KTHXBYE.Match (line).Success) {
				if (long_comment == 1) {
					printError (4, lineIndex, null);
					break;
				} else {
					LexemeStore.AppendValues ("KTHXBYE", "Code Delimiter");
				}
			} else if (ITZ.Match (line).Success) {
				String variable_declaration_keyword = ITZ.Match (line).Groups ["vardec"].Value.ToString ();
				String identifier = ITZ.Match (line).Groups ["identifier"].Value.ToString ();
				String itz = ITZ.Match (line).Groups ["itz"].Value.ToString ();
				String value = ITZ.Match (line).Groups ["variable_type"].Value.ToString ();

				if (RESERVED.Match (identifier).Success) {
					printError (6, lineIndex, null);
					break;
				}
				if (RESERVED.Match (value).Success) {
					printError (7, lineIndex, null);
					break;
				}

				int repeated = 0;
				if (variables.ContainsKey (identifier)) {
					repeated = 1;
					printError (2, lineIndex, identifier);
					break;
				}

				if (repeated != 1) {
					LexemeStore.AppendValues (variable_declaration_keyword, "Variable Declaration Keyword");
					LexemeStore.AppendValues (identifier, "Variable Identifier");
					LexemeStore.AppendValues (itz, "Value Assignment Identifier");
					checkType (value);

					variables.Add (identifier, 0);
					SymbolTableStore.AppendValues (identifier, "", value);
				}
			} else if (I_HAS_A.Match (line).Success) {
				String variable_declaration_keyword = I_HAS_A.Match (line).Groups ["vardec"].Value.ToString ();
				String identifier = I_HAS_A.Match (line).Groups ["identifier"].Value.ToString ();

				if (RESERVED.Match (identifier).Success) {
					printError (6, lineIndex, null);
					break;
				}

				int repeated = 0;
				if (variables.ContainsKey (identifier)) {
					repeated = 1;

					printError (2, lineIndex, identifier);
					break;
				}

				if (repeated == 1) break;
				else {
					LexemeStore.AppendValues (variable_declaration_keyword, "Variable Declaration Keyword");
					LexemeStore.AppendValues (identifier, "Variable Identifier");

					variables.Add (identifier, 0);
					//SymbolTableStore.AppendValues (identifier, " ", " ");
				}
			} else if (BTW.Match (line).Success) {
				String btw = BTW.Match (line).Groups ["comment"].Value.ToString ();

				LexemeStore.AppendValues (btw, "Comment Declaration");
			} else if (OBTW.Match (line).Success) {
				LexemeStore.AppendValues ("OBTW", "Multi-Line Comment Starter");

				long_comment = 1;
			} else if (TLDR.Match (line).Success) {
				if (long_comment == 1) {
					String tldr = TLDR.Match (line).Groups ["comment"].Value.ToString ();

					LexemeStore.AppendValues (tldr, "Multi-Line Comment Ender");
				} else {
					printError (5, lineIndex, null);
					break;
				}
				long_comment = 0;
			} else if (R.Match (line).Success) {
				String r = R.Match (line).Groups ["vardec"].Value.ToString ();
				String value = R.Match (line).Groups ["identifier"].Value.ToString ();

				LexemeStore.AppendValues (r, "Assignment Statement");
				LexemeStore.AppendValues (value, "Value");
			} else if (SUM_OF.Match (line).Success) {
				addition (line);
			} else if (DIFF_OF.Match (line).Success) {
				subtraction (line);
			} else if (PRODUKT_OF.Match (line).Success) {
				multiplication(line);
			} else if (QUOSHUNT_OF.Match (line).Success) {
				division(line);
			} else if (MOD_OF.Match (line).Success) {
				modulo(line);
			} else if (BIGGR_OF.Match (line).Success) {
				bigger (line);
			} else if (SMALLR_OF.Match (line).Success) {
				smaller (line);
			} else if (BOTH_OF.Match (line).Success) {
				String keyword = BOTH_OF.Match (line).Groups ["keyword"].Value.ToString ();
				String factor1 = BOTH_OF.Match (line).Groups ["operand_1"].Value.ToString ();
				String and = BOTH_OF.Match (line).Groups ["and"].Value.ToString ();
				String factor2 = BOTH_OF.Match (line).Groups ["operand_2"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "Logical AND Keyword");
				checkType (factor1);
				LexemeStore.AppendValues (and, "Separator Keyword");
				checkType (factor2);
			} else if (EITHER_OF.Match (line).Success) {
				String keyword = EITHER_OF.Match (line).Groups ["keyword"].Value.ToString ();
				String factor1 = EITHER_OF.Match (line).Groups ["operand_1"].Value.ToString ();
				String and = EITHER_OF.Match (line).Groups ["and"].Value.ToString ();
				String factor2 = EITHER_OF.Match (line).Groups ["operand_2"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "Logical OR Keyword");
				checkType (factor1);
				LexemeStore.AppendValues (and, "Separator Keyword");
				checkType (factor2);
			} else if (WON_OF.Match (line).Success) {
				String keyword = WON_OF.Match (line).Groups ["keyword"].Value.ToString ();
				String factor1 = WON_OF.Match (line).Groups ["operand_1"].Value.ToString ();
				String and = WON_OF.Match (line).Groups ["and"].Value.ToString ();
				String factor2 = WON_OF.Match (line).Groups ["operand_2"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "Logical XOR Keyword");
				checkType (factor1);
				LexemeStore.AppendValues (and, "Separator Keyword");
				checkType (factor2);
			} else if (NOT.Match (line).Success) {
				String keyword = NOT.Match (line).Groups ["keyword"].Value.ToString ();
				String factor = NOT.Match (line).Groups ["operand"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "Logical NOT Keyword");
				checkType (factor);
			} else if (ALL_OF.Match (line).Success) {
				String keyword = ALL_OF.Match (line).Groups ["keyword"].Value.ToString ();
				String factor1 = ALL_OF.Match (line).Groups ["operand_1"].Value.ToString ();
				String and = ALL_OF.Match (line).Groups ["and"].Value.ToString ();
				String factor2 = ALL_OF.Match (line).Groups ["operand_2"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "Logical AND Keyword");
				checkType (factor1);
				LexemeStore.AppendValues (and, "Separator Keyword");
				checkType (factor2);
			} else if (ANY_OF.Match (line).Success) {
				String keyword = ANY_OF.Match (line).Groups ["keyword"].Value.ToString ();
				String factor1 = ANY_OF.Match (line).Groups ["operand_1"].Value.ToString ();
				String and = ANY_OF.Match (line).Groups ["and"].Value.ToString ();
				String factor2 = ANY_OF.Match (line).Groups ["operand_2"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "Logical OR Keyword");
				checkType (factor1);
				LexemeStore.AppendValues (and, "Separator Keyword");
				checkType (factor2);
			} else if (BOTH_SAEM.Match (line).Success) {
				String keyword = BOTH_SAEM.Match (line).Groups ["keyword"].Value.ToString ();
				String factor1 = BOTH_SAEM.Match (line).Groups ["part1"].Value.ToString ();
				String and = BOTH_SAEM.Match (line).Groups ["and"].Value.ToString ();
				String factor2 = BOTH_SAEM.Match (line).Groups ["part2"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "Equality Keyword");
				checkType (factor1);
				LexemeStore.AppendValues (and, "Separator Keyword");
				checkType (factor2);
			} else if (DIFFRINT.Match (line).Success) {
				String keyword = DIFFRINT.Match (line).Groups ["keyword"].Value.ToString ();
				String factor1 = DIFFRINT.Match (line).Groups ["part1"].Value.ToString ();
				String and = DIFFRINT.Match (line).Groups ["and"].Value.ToString ();
				String factor2 = DIFFRINT.Match (line).Groups ["part2"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "Inequality Keyword");
				checkType (factor1);
				LexemeStore.AppendValues (and, "Separator Keyword");
				checkType (factor2);
			} else if (SMOOSH.Match (line).Success) {
				String keyword = SMOOSH.Match (line).Groups ["keyword"].Value.ToString ();
				String factor1 = SMOOSH.Match (line).Groups ["part1"].Value.ToString ();
				String and = SMOOSH.Match (line).Groups ["and"].Value.ToString ();
				String factor2 = SMOOSH.Match (line).Groups ["part2"].Value.ToString ();
				String keyword2 = SMOOSH.Match (line).Groups ["keyword2"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "String Concatenation Keyword");
				checkType (factor1);
				LexemeStore.AppendValues (and, "Separator Keyword");
				checkType (factor2);
				LexemeStore.AppendValues (keyword2, "String Concatenation Delimiter");
			} else if (MAEK.Match (line).Success) {
				String keyword = MAEK.Match (line).Groups ["keyword"].Value.ToString ();
				String factor1 = MAEK.Match (line).Groups ["part1"].Value.ToString ();
				String and = MAEK.Match (line).Groups ["and"].Value.ToString ();
				String factor2 = MAEK.Match (line).Groups ["part2"].Value.ToString ();

				LexemeStore.AppendValues (keyword, "Value Converter Keyword");
				checkType (factor1);
				LexemeStore.AppendValues (and, "Separator Keyword");
				checkType (factor2);
			} else if (IS_NOW_A.Match (line).Success) {
				String factor1 = IS_NOW_A.Match (line).Groups ["part1"].Value.ToString ();
				String keyword = IS_NOW_A.Match (line).Groups ["keyword"].Value.ToString ();
				String factor2 = IS_NOW_A.Match (line).Groups ["part2"].Value.ToString ();

				checkType (factor1);
				LexemeStore.AppendValues (keyword, "Type Converter Keyword");
				checkType (factor2);
			} else if (VISIBLE.Match (line).Success) {
				String keyword = VISIBLE.Match (line).Groups ["keyword"].Value.ToString ();
				String statement = VISIBLE.Match (line).Groups ["identifier"].Value.ToString ();

				int repeated = 0;
				if (variables.ContainsKey (statement)) {
					repeated = 1;
					LexemeStore.AppendValues (keyword, "Output Identifier");
					LexemeStore.AppendValues (statement, "Variable");
				}

				if (repeated == 1) break;
				else {
					char[] mychar = { '"' };
					string mychar2 = "\"";
					string identifier = statement.TrimStart (mychar);
					identifier = identifier.TrimEnd (mychar);

					LexemeStore.AppendValues (keyword, "Output Identifier");
					LexemeStore.AppendValues (mychar2, "Delimiter");
					LexemeStore.AppendValues (identifier, "Output");
					LexemeStore.AppendValues (mychar2, "Delimiter");

					string output = identifier;
					printToConsole (output);
				}
			} else if (VISIBLE2.Match (line).Success) {
				String keyword = VISIBLE2.Match (line).Groups ["keyword"].Value.ToString ();
				String statement = VISIBLE2.Match (line).Groups ["identifier"].Value.ToString ();

				int repeated = 0;
				if (variables.ContainsKey (statement)) {
					repeated = 1;
					LexemeStore.AppendValues (keyword, "Output Identifier");
					LexemeStore.AppendValues (statement, "Variable");
				}

				if (repeated != 1) {
					printError(1, lineIndex, null);
					break;
				}
			} else if (GIMMEH.Match (line).Success) {
				String keyword = GIMMEH.Match (line).Groups ["keyword"].Value.ToString ();
				String identifier = GIMMEH.Match (line).Groups ["identifier"].Value.ToString ();

				int repeated = 0;
				if (variables.ContainsKey (identifier)) {
					repeated = 1;
					String scan = Interaction.InputBox("Enter Input", "GIMMEH", null, -1, -1);
					LexemeStore.AppendValues (keyword, "Input Identifier");
					LexemeStore.AppendValues (identifier, "Input");
					SymbolTableStore.AppendValues (identifier, "", scan);
				}
					
				if (repeated != 1) {
					printError(1, lineIndex, null);
					break;
				}
			} else if(ORLY.Match(line).Success){
				foreach (String s in code_lines.Skip(i+1)){
					if(YARLY.Match(s).Success){
						foreach (String t in code_lines.Skip(i+1)){
							if(OIC.Match(t).Success){
								LexemeStore.AppendValues("O RLY?", "If Statement");	
								LexemeStore.AppendValues("YA RLY", "Executed if true(WIN) is evaluated");
								LexemeStore.AppendValues("OIC", "End of if-clause");
								err = 1;
							}}}}
				if(err != 1){
					printError (8, lineIndex, null);
					break;
				}
			} else if(MEBBE.Match(line).Success){
				string expression = MEBBE.Match(line).Groups["expression"].Value.ToString();

				LexemeStore.AppendValues("MEBBE", "If expression following MEBBE is WIN, performs block");
				LexemeStore.AppendValues(expression, "Expression");
			} else if(WTF.Match(line).Success){
				foreach (String s in code_lines.Skip(i+1)){
					if(OMG.Match(s).Success){
						string identifier = OMG.Match(s).Groups["identifier"].Value.ToString();
						LexemeStore.AppendValues("OMG?", "Case");
						LexemeStore.AppendValues(identifier, "Case no");
					}}	
				foreach (String t in code_lines.Skip(i+1)){
					if(OIC.Match(t).Success){
						LexemeStore.AppendValues("WTF", "Switch statement");
						LexemeStore.AppendValues("OIC", "End of clause");
						err = 1;
					}
				}
				if(err != 1){
					printError (9, lineIndex, null);
					break;
				}

			} else if(OMGWTF.Match(line).Success){
				LexemeStore.AppendValues("OMGWTF?", "Comparison Block");
			} else if(IMINYR.Match(line).Success){
				string label = IMINYR.Match(line).Groups["label"].Value.ToString();
				string operation = IMINYR.Match(line).Groups["operation"].Value.ToString();
				string your = IMINYR.Match(line).Groups["your"].Value.ToString();
				string variable = IMINYR.Match(line).Groups["variable"].Value.ToString();
				string eval = IMINYR.Match(line).Groups["eval"].Value.ToString();
				string expression = IMINYR.Match(line).Groups["expression"].Value.ToString();

				foreach (String s in code_lines.Skip(i+1)){
					if(IMOUTTAYR.Match(s).Success){
						string identifier = IMOUTTAYR.Match(s).Groups["identifier"].Value.ToString();

						LexemeStore.AppendValues("IM IN YR", "While Loop");
						LexemeStore.AppendValues(label, "Label");
						LexemeStore.AppendValues(operation, "While Operation");
						LexemeStore.AppendValues(your, "Refers to while expression");
						LexemeStore.AppendValues(variable, "Variable");
						LexemeStore.AppendValues(eval, "Evaluator");
						LexemeStore.AppendValues(expression, "Expression");
						LexemeStore.AppendValues("IM OUTTA YR", "Exits while loop");
						LexemeStore.AppendValues(identifier, "Case Value");
						err = 1;
						if(label!=identifier){
							err = 0;	
						}
					}
				}
				if(err != 1){
					printError (10, lineIndex, null);
					break;
				}				
			} else if (long_comment != 1){
				printError (1, lineIndex, null);
				break;
			} 

			err = 0;
		}
	}

	private void checkType(String str)
	{
		if (TROOF.Match (str).Success) {
			LexemeStore.AppendValues (str, "TROOF Literal");
			//vartype.Add (str, "TROOF");
		} else if (NUMBAR.Match (str).Success && !str.Contains (" ")) {
			LexemeStore.AppendValues (str, "NUMBAR Literal");
			//vartype.Add (str, "NUMBAR");
		} else if (NUMBR.Match (str).Success && !str.Contains (" ")) {
			LexemeStore.AppendValues (str, "NUMBR Literal");
			//vartype.Add (str, "NUMBR");
		} else if (VAR_NAME.Match (str).Success && !str.Contains (" ")) {
			LexemeStore.AppendValues (str, "Variable Identifier");
			//vartype.Add (str, "VARIABLE");
		} else if (YARN.Match (str).Success) {
			LexemeStore.AppendValues (str, "YARN Literal");
			//vartype.Add (str, "YARN");
		} else {
			LexemeStore.AppendValues (str, "NOOB Literal");
			//vartype.Add (str, "NOOB");
		}
	}

	public void printToConsole(String text) {
		String previous_content = Console.Buffer.Text;
		Console.Buffer.Text = previous_content + text + Environment.NewLine;
	}

	public void addition(String line) {

		Stack myStack = new Stack ();

		String keyword = SUM_OF.Match (line).Groups ["keyword"].Value.ToString ();
		String factor1 = SUM_OF.Match (line).Groups ["operand_1"].Value.ToString ();
		String and = SUM_OF.Match (line).Groups ["and"].Value.ToString ();
		String factor2 = SUM_OF.Match (line).Groups ["operand_2"].Value.ToString ();

		LexemeStore.AppendValues (keyword, "Addition Keyword");

		//LexemeStore.AppendValues ("Factor 1 is ", factor1);

		if (SUM_OF_EXPRESSION.Match (factor1).Success) {
			addition (factor1);
		} else if (DIFF_OF_EXPRESSION.Match (factor1).Success) {
			subtraction (factor1);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor1).Success) {
			multiplication (factor1);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor1).Success) {
			division (factor1);
		} else if (MOD_OF_EXPRESSION.Match (factor1).Success) {
			modulo (factor1);
		} else if (BIGGR_OF_EXPRESSION.Match (factor1).Success) {
			bigger (factor1);
		} else if (SMALLR_OF_EXPRESSION.Match (factor1).Success) {
			smaller (factor1);
		} else {
			checkType (factor1);
			myStack.Push (factor1);
			LexemeStore.AppendValues ("Factor 1 is ", factor1);
		}
			
		if (SUM_OF_EXPRESSION.Match (factor2).Success) {
			addition (factor2);
		} else if (DIFF_OF_EXPRESSION.Match (factor2).Success) {
			subtraction (factor2);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor2).Success) {
			multiplication (factor2);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor2).Success) {
			division (factor2);
		} else if (MOD_OF_EXPRESSION.Match (factor2).Success) {
			modulo (factor2);
		} else if (BIGGR_OF_EXPRESSION.Match (factor2).Success) {
			bigger (factor2);
		} else if (SMALLR_OF_EXPRESSION.Match (factor2).Success) {
			smaller (factor2);
		} 

			LexemeStore.AppendValues (and, "Separator Keyword");
			checkType (factor2);
			myStack.Push (factor2);
			LexemeStore.AppendValues ("Factor 2 is ", factor2);

		int x = Convert.ToInt32 (myStack.Pop());
		int z = Convert.ToInt32 (myStack.Pop());

		int sum = x + z;
		printToConsole ("Sum is " + sum);
		LexemeStore.AppendValues ("ANG SAGOT AY", sum.ToString());
		//foreach (int i in myStack) {
		//	printToConsole (i);
		//} 

	}

	public void subtraction(String line) {
		String keyword = DIFF_OF.Match (line).Groups ["keyword"].Value.ToString ();
		String factor1 = DIFF_OF.Match (line).Groups ["operand_1"].Value.ToString ();
		String and = DIFF_OF.Match (line).Groups ["and"].Value.ToString ();
		String factor2 = DIFF_OF.Match (line).Groups ["operand_2"].Value.ToString ();

		LexemeStore.AppendValues (keyword, "Subtraction Keyword");

		if (SUM_OF_EXPRESSION.Match (factor1).Success) {
			addition (factor1);
		} else if (DIFF_OF_EXPRESSION.Match (factor1).Success) {
			subtraction (factor1);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor1).Success) {
			multiplication (factor1);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor1).Success) {
			division (factor1);
		} else if (MOD_OF_EXPRESSION.Match (factor1).Success) {
			modulo (factor1);
		} else if (BIGGR_OF_EXPRESSION.Match (factor1).Success) {
			bigger (factor1);
		} else if (SMALLR_OF_EXPRESSION.Match (factor1).Success) {
			smaller (factor1);
		} else {
			checkType (factor1);
		}

		if (SUM_OF_EXPRESSION.Match (factor2).Success) {
			addition (factor2);
		} else if (DIFF_OF_EXPRESSION.Match (factor2).Success) {
			subtraction (factor2);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor2).Success) {
			multiplication (factor2);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor2).Success) {
			division(factor2);
		} else if (MOD_OF_EXPRESSION.Match (factor2).Success) {
			modulo(factor2);
		} else if (BIGGR_OF_EXPRESSION.Match (factor2).Success) {
			bigger(factor2);
		} else if (SMALLR_OF_EXPRESSION.Match (factor2).Success) {
			smaller(factor2);
		} 

		LexemeStore.AppendValues (and, "Separator Keyword");
		checkType (factor2);
	}

	public void multiplication(String line) {
		String keyword = PRODUKT_OF.Match (line).Groups ["keyword"].Value.ToString ();
		String factor1 = PRODUKT_OF.Match (line).Groups ["operand_1"].Value.ToString ();
		String and = PRODUKT_OF.Match (line).Groups ["and"].Value.ToString ();
		String factor2 = PRODUKT_OF.Match (line).Groups ["operand_2"].Value.ToString ();

		LexemeStore.AppendValues (keyword, "Multiplication Keyword");

		if (SUM_OF_EXPRESSION.Match (factor1).Success) {
			addition (factor1);
		} else if (DIFF_OF_EXPRESSION.Match (factor1).Success) {
			subtraction (factor1);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor1).Success) {
			multiplication (factor1);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor1).Success) {
			division(factor1);
		} else if (MOD_OF_EXPRESSION.Match (factor1).Success) {
			modulo(factor1);
		} else if (BIGGR_OF_EXPRESSION.Match (factor1).Success) {
			bigger(factor1);
		} else if (SMALLR_OF_EXPRESSION.Match (factor1).Success) {
			smaller(factor1);
		} 

		checkType (factor1);

		if (SUM_OF_EXPRESSION.Match (factor2).Success) {
			addition (factor2);
		} else if (DIFF_OF_EXPRESSION.Match (factor2).Success) {
			subtraction (factor2);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor2).Success) {
			multiplication (factor2);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor2).Success) {
			division(factor2);
		} else if (MOD_OF_EXPRESSION.Match (factor2).Success) {
			modulo(factor2);
		} else if (BIGGR_OF_EXPRESSION.Match (factor2).Success) {
			bigger(factor2);
		} else if (SMALLR_OF_EXPRESSION.Match (factor2).Success) {
			smaller(factor2);
		} 

		LexemeStore.AppendValues (and, "Separator Keyword");
		checkType (factor2);
	}

	public void division(String line) {
		String keyword = QUOSHUNT_OF.Match (line).Groups ["keyword"].Value.ToString ();
		String factor1 = QUOSHUNT_OF.Match (line).Groups ["operand_1"].Value.ToString ();
		String and = QUOSHUNT_OF.Match (line).Groups ["and"].Value.ToString ();
		String factor2 = QUOSHUNT_OF.Match (line).Groups ["operand_2"].Value.ToString ();

		LexemeStore.AppendValues (keyword, "Division Keyword");

		if (SUM_OF_EXPRESSION.Match (factor1).Success) {
			addition (factor1);
		} else if (DIFF_OF_EXPRESSION.Match (factor1).Success) {
			subtraction (factor1);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor1).Success) {
			multiplication (factor1);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor1).Success) {
			division(factor1);
		} else if (MOD_OF_EXPRESSION.Match (factor1).Success) {
			modulo(factor1);
		} else if (BIGGR_OF_EXPRESSION.Match (factor1).Success) {
			bigger(factor1);
		} else if (SMALLR_OF_EXPRESSION.Match (factor1).Success) {
			smaller(factor1);
		} 

		checkType (factor1);

		if (SUM_OF_EXPRESSION.Match (factor2).Success) {
			addition (factor2);
		} else if (DIFF_OF_EXPRESSION.Match (factor2).Success) {
			subtraction (factor2);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor2).Success) {
			multiplication (factor2);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor2).Success) {
			division(factor2);
		} else if (MOD_OF_EXPRESSION.Match (factor2).Success) {
			modulo(factor2);
		} else if (BIGGR_OF_EXPRESSION.Match (factor2).Success) {
			bigger(factor2);
		} else if (SMALLR_OF_EXPRESSION.Match (factor2).Success) {
			smaller(factor2);
		} 

		LexemeStore.AppendValues (and, "Separator Keyword");
		checkType (factor2);
	}

	public void modulo(String line) {
		String keyword = MOD_OF.Match (line).Groups ["keyword"].Value.ToString ();
		String factor1 = MOD_OF.Match (line).Groups ["operand_1"].Value.ToString ();
		String and = MOD_OF.Match (line).Groups ["and"].Value.ToString ();
		String factor2 = MOD_OF.Match (line).Groups ["operand_2"].Value.ToString ();

		LexemeStore.AppendValues (keyword, "Modulo Keyword");

		if (SUM_OF_EXPRESSION.Match (factor1).Success) {
			addition (factor1);
		} else if (DIFF_OF_EXPRESSION.Match (factor1).Success) {
			subtraction (factor1);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor1).Success) {
			multiplication (factor1);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor1).Success) {
			division(factor1);
		} else if (MOD_OF_EXPRESSION.Match (factor1).Success) {
			modulo(factor1);
		} else if (BIGGR_OF_EXPRESSION.Match (factor1).Success) {
			bigger(factor1);
		} else if (SMALLR_OF_EXPRESSION.Match (factor1).Success) {
			smaller(factor1);
		} 

		checkType (factor1);

		if (SUM_OF_EXPRESSION.Match (factor2).Success) {
			addition (factor2);
		} else if (DIFF_OF_EXPRESSION.Match (factor2).Success) {
			subtraction (factor2);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor2).Success) {
			multiplication (factor2);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor2).Success) {
			division(factor2);
		} else if (MOD_OF_EXPRESSION.Match (factor2).Success) {
			modulo(factor2);
		} else if (BIGGR_OF_EXPRESSION.Match (factor2).Success) {
			bigger(factor2);
		} else if (SMALLR_OF_EXPRESSION.Match (factor2).Success) {
			smaller(factor2);
		} 

		LexemeStore.AppendValues (and, "Separator Keyword");
		checkType (factor2);
	}

	public void bigger(String line) {
		String keyword = BIGGR_OF.Match (line).Groups ["keyword"].Value.ToString ();
		String factor1 = BIGGR_OF.Match (line).Groups ["operand_1"].Value.ToString ();
		String and = BIGGR_OF.Match (line).Groups ["and"].Value.ToString ();
		String factor2 = BIGGR_OF.Match (line).Groups ["operand_2"].Value.ToString ();

		LexemeStore.AppendValues (keyword, "Comparison Keyword");

		if (SUM_OF_EXPRESSION.Match (factor1).Success) {
			addition (factor1);
		} else if (DIFF_OF_EXPRESSION.Match (factor1).Success) {
			subtraction (factor1);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor1).Success) {
			multiplication (factor1);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor1).Success) {
			division(factor1);
		} else if (MOD_OF_EXPRESSION.Match (factor1).Success) {
			modulo(factor1);
		} else if (BIGGR_OF_EXPRESSION.Match (factor1).Success) {
			bigger(factor1);
		} else if (SMALLR_OF_EXPRESSION.Match (factor1).Success) {
			smaller(factor1);
		} 

		checkType (factor1);

		if (SUM_OF_EXPRESSION.Match (factor2).Success) {
			addition (factor2);
		} else if (DIFF_OF_EXPRESSION.Match (factor2).Success) {
			subtraction (factor2);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor2).Success) {
			multiplication (factor2);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor2).Success) {
			division(factor2);
		} else if (MOD_OF_EXPRESSION.Match (factor2).Success) {
			modulo(factor2);
		} else if (BIGGR_OF_EXPRESSION.Match (factor2).Success) {
			bigger(factor2);
		} else if (SMALLR_OF_EXPRESSION.Match (factor2).Success) {
			smaller(factor2);
		} 

		LexemeStore.AppendValues (and, "Separator Keyword");
		checkType (factor2);
	}

	public void smaller(String line) {
		String keyword = SMALLR_OF.Match (line).Groups ["keyword"].Value.ToString ();
		String factor1 = SMALLR_OF.Match (line).Groups ["operand_1"].Value.ToString ();
		String and = SMALLR_OF.Match (line).Groups ["and"].Value.ToString ();
		String factor2 = SMALLR_OF.Match (line).Groups ["operand_2"].Value.ToString ();

		LexemeStore.AppendValues (keyword, "Comparison Keyword");

		if (SUM_OF_EXPRESSION.Match (factor1).Success) {
			addition (factor1);
		} else if (DIFF_OF_EXPRESSION.Match (factor1).Success) {
			subtraction (factor1);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor1).Success) {
			multiplication (factor1);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor1).Success) {
			division(factor1);
		} else if (MOD_OF_EXPRESSION.Match (factor1).Success) {
			modulo(factor1);
		} else if (BIGGR_OF_EXPRESSION.Match (factor1).Success) {
			bigger(factor1);
		} else if (SMALLR_OF_EXPRESSION.Match (factor1).Success) {
			smaller(factor1);
		} 

		checkType (factor1);

		if (SUM_OF_EXPRESSION.Match (factor2).Success) {
			addition (factor2);
		} else if (DIFF_OF_EXPRESSION.Match (factor2).Success) {
			subtraction (factor2);
		} else if (PRODUKT_OF_EXPRESSION.Match (factor2).Success) {
			multiplication (factor2);
		} else if (QUOSHUNT_OF_EXPRESSION.Match (factor2).Success) {
			division(factor2);
		} else if (MOD_OF_EXPRESSION.Match (factor2).Success) {
			modulo(factor2);
		} else if (BIGGR_OF_EXPRESSION.Match (factor2).Success) {
			bigger(factor2);
		} else if (SMALLR_OF_EXPRESSION.Match (factor2).Success) {
			smaller(factor2);
		} 

		LexemeStore.AppendValues (and, "Separator Keyword");
		checkType (factor2);
	}

	public void printError(int error_num, int lineIndex, String var_name)
	{
		switch (error_num)
		{
		case 0: // lexical analysis error, no/incomplete program delimeter
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "   ERROR: Code must start with a HAI and end with a KTHXBYE." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 1: // lexical analysis error, syntax error
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "   ERROR: In line " + lineIndex.ToString() + ", an error in the syntax was detected." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 2: // variable declaration error, variable already exists
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "   ERROR: In line " + lineIndex.ToString() + ", the variable " + var_name + " already exists." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 3: // oic error
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "   ERROR: In line " + lineIndex.ToString() + ", missing OIC keyword" + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 4: // tldr error
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "   ERROR: In line " + lineIndex.ToString() + ", missing TLDR keyword." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 5: // obtw error
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "   ERROR: In line " + lineIndex.ToString() + ", missing OBTW keyword." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 6: // reserved word error (variable)
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "   ERROR: In line " + lineIndex.ToString() + ", you cannot use a reserved word as a variable name." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 7: // reserved word error (value)
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "  ERROR: In line " + lineIndex.ToString() + ", you cannot use a reserved word as a value." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 8: // if-statement error
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "  ERROR: In line " + lineIndex.ToString() + ", there is an error in the if statement." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 9: // switch case statement error
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "  ERROR: In line " + lineIndex.ToString() + ", there is an error in the switch statement." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		case 10: // loop error
			Console.Buffer.Text += Environment.NewLine + "======================================================================" + Environment.NewLine + "  ERROR: In line " + lineIndex.ToString() + ", there is an error in the loop statement." + Environment.NewLine + "======================================================================" + Environment.NewLine;
			break;
		default:
			break;
		}
	}
}
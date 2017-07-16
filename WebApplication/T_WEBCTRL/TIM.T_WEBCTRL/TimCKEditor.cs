using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIM.T_WEBCTRL
{
	[DefaultProperty("Text"), Designer("TIM.T_WEBCTRL.TimCKEditorDesigner"), ParseChildren(false), ToolboxData("<{0}:TimCKEditor runat=server></{0}:TimCKEditor>")]
	public class TimCKEditor : TextBox, IPostBackDataHandler
	{
		private bool isChanged = false;

		private static bool? _HasMsAjax = null;

		private static Type typScriptManager;

		private static Type updatePanel;

		private static MethodInfo mtdRegisterClientScriptInclude;

		private static MethodInfo mtdRegisterStartupScript;

		private static MethodInfo mtdRegisterOnSubmitStatement;

		private string timestamp = "?t=C6HH5UF";

		[Bindable(true), Category("CKEditor Basic Settings"), DefaultValue("")]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		public override TextBoxMode TextMode
		{
			get
			{
				return base.TextMode;
			}
		}

		[DefaultValue(false), Description("If true, makes the editor start in read-only state. Otherwise, it will check if the linked <textarea> element has the disabled attribute.")]
		public override bool ReadOnly
		{
			get
			{
				return this.config.readOnly;
			}
			set
			{
				CKEditorConfig arg_12_0 = this.config;
				base.ReadOnly = value;
				arg_12_0.readOnly = value;
			}
		}

		private string CKEditorJSFile
		{
			get
			{
				return this.BasePath + "/ckeditor.js";
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("~/Scripts/ckeditor")]
		public string BasePath
		{
			get
			{
				object obj = this.ViewState["BasePath"];
				bool flag = obj == null;
				if (flag)
				{
					obj = ConfigurationManager.AppSettings["CKeditor:BasePath"];
					this.ViewState["BasePath"] = ((obj == null) ? "~/Scripts/ckeditor" : ((string)obj));
				}
				return (string)this.ViewState["BasePath"];
			}
			set
			{
				bool flag = value.EndsWith("/");
				if (flag)
				{
					value = value.Remove(value.Length - 1);
				}
				this.ViewState["BasePath"] = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public CKEditorConfig config
		{
			get
			{
				bool flag = this.ViewState["CKEditorConfig"] == null;
				if (flag)
				{
					this.ViewState["CKEditorConfig"] = new CKEditorConfig(this.BasePath.StartsWith("~") ? base.ResolveUrl(this.BasePath) : this.BasePath);
				}
				return (CKEditorConfig)this.ViewState["CKEditorConfig"];
			}
			set
			{
				this.ViewState["CKEditorConfig"] = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(typeof(Unit), "200px"), Description("The height of editing area( content ), in relative or absolute, e.g. 30px, 5em. Note: Percentage unit is not supported yet.e.g. 30%.")]
		public override Unit Height
		{
			get
			{
				Unit result = new Unit(string.Empty);
				try
				{
					result = Unit.Parse(this.config.height);
					base.Height = result;
				}
				catch
				{
				}
				return result;
			}
			set
			{
				Unit result = new Unit(string.Empty);
				try
				{
					result = value;
					base.Height = result;
				}
				catch
				{
				}
				this.config.height = value.ToString().Replace("px", string.Empty);
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(typeof(Unit), "100%"), Description("The editor width in CSS size format or pixel integer.")]
		public override Unit Width
		{
			get
			{
				Unit result = new Unit(string.Empty);
				try
				{
					result = Unit.Parse(this.config.width);
					base.Width = result;
				}
				catch
				{
				}
				return result;
			}
			set
			{
				Unit result = new Unit(string.Empty);
				try
				{
					result = value;
					base.Width = result;
				}
				catch
				{
				}
				this.config.width = value.ToString().Replace("px", string.Empty);
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(short), "0"), Description("The editor tabindex value.")]
		public override short TabIndex
		{
			get
			{
				return (short)this.config.tabIndex;
			}
			set
			{
				this.config.tabIndex = (int)value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(0), Description("Extra height in pixel to leave between the bottom boundary of content with document size when auto resizing.")]
		public int AutoGrowBottomSpace
		{
			get
			{
				return this.config.autoGrow_bottomSpace;
			}
			set
			{
				this.config.autoGrow_bottomSpace = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(false), Description("Whether to have the auto grow happen on editor creation.")]
		public bool AutoGrowOnStartup
		{
			get
			{
				return this.config.autoGrow_onStartup;
			}
			set
			{
				this.config.autoGrow_onStartup = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(0), Description("The maximum height to which the editor can reach using AutoGrow. Zero means unlimited.")]
		public int AutoGrowMaxHeight
		{
			get
			{
				return this.config.autoGrow_maxHeight;
			}
			set
			{
				this.config.autoGrow_maxHeight = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(200), Description("The minimum height to which the editor can reach using AutoGrow.")]
		public int AutoGrowMinHeight
		{
			get
			{
				return this.config.autoGrow_minHeight;
			}
			set
			{
				this.config.autoGrow_minHeight = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether automatically create wrapping blocks around inline contents inside document body, this helps to ensure the integrality of the block enter mode. Note: Changing the default value might introduce unpredictable usability issues.")]
		public bool AutoParagraph
		{
			get
			{
				return this.config.autoParagraph;
			}
			set
			{
				this.config.autoParagraph = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether the replaced element (usually a textarea) is to be updated automatically when posting the form containing the editor.")]
		public bool AutoUpdateElement
		{
			get
			{
				return this.config.autoUpdateElement;
			}
			set
			{
				this.config.autoUpdateElement = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether to escape basic HTML entities in the document, including: nbsp, gt, lt, amp.")]
		public bool BasicEntities
		{
			get
			{
				return this.config.basicEntities;
			}
			set
			{
				this.config.basicEntities = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("The base href URL used to resolve relative and absolute URLs in the editor content.")]
		public string BaseHref
		{
			get
			{
				return this.config.baseHref;
			}
			set
			{
				this.config.baseHref = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("Sets the \"class\" attribute to be used on the body element of the editing area. This can be useful when reusing the original CSS\r\nfile you're using on your live website and you want to assing to the editor the same class name you're using for the region \r\nthat'll hold the contents. In this way, class specific CSS rules will be enabled.")]
		public string BodyClass
		{
			get
			{
				return this.config.bodyClass;
			}
			set
			{
				this.config.bodyClass = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("Sets the \"id\" attribute to be used on the body element of the editing area. This can be useful when reusing the original CSS \r\nfile you're using on your live website and you want to assing to the editor the same id you're using for the region \r\nthat'll hold the contents. In this way, id specific CSS rules will be enabled.")]
		public string BodyId
		{
			get
			{
				return this.config.bodyId;
			}
			set
			{
				this.config.bodyId = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether to show the browser native context menu when the CTRL or the META (Mac) key is pressed while opening the context menu.")]
		public bool BrowserContextMenuOnCtrl
		{
			get
			{
				return this.config.browserContextMenuOnCtrl;
			}
			set
			{
				this.config.browserContextMenuOnCtrl = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("~/Scripts/ckeditor/contents.css"), Description("The CSS file(s) to be used to apply style to the contents. It should reflect the CSS used in the final pages where the contents are to be used."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), PersistenceMode(PersistenceMode.Attribute)]
		public string ContentsCss
		{
			get
			{
				string retVal = string.Empty;
				string[] contentsCss = this.config.contentsCss;
				for (int i = 0; i < contentsCss.Length; i++)
				{
					string item = contentsCss[i];
					retVal = retVal + item + ",";
				}
				bool flag = retVal.EndsWith(",");
				if (flag)
				{
					retVal = retVal.Remove(retVal.Length - 1);
				}
				return retVal;
			}
			set
			{
				this.config.contentsCss = value.Replace("\t", string.Empty).Split(new char[]
				{
					',',
					'\n',
					'\r'
				}, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(contentsLangDirections), "Ui"), Description("The writing direction of the language used to write the editor contents. Allowed values are: \r\n'ui' - which indicate content direction will be the same with the user interface language direction;\r\n'ltr' - for Left-To-Right language (like English);\r\n'rtl' - for Right-To-Left languages (like Arabic).")]
		public contentsLangDirections ContentsLangDirection
		{
			get
			{
				return this.config.contentsLangDirection;
			}
			set
			{
				this.config.contentsLangDirection = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("Language code of the writting language which is used to author the editor contents.")]
		public string ContentsLanguage
		{
			get
			{
				return this.config.contentsLanguage;
			}
			set
			{
				this.config.contentsLanguage = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("config.js"), Description("The URL path for the custom configuration file to be loaded. If not overloaded with inline configurations, it defaults \r\nto the \"config.js\" file present in the root of the CKEditor installation directory.\r\nCKEditor will recursively load custom configuration files defined inside other custom configuration files.")]
		public string CustomConfig
		{
			get
			{
				return this.config.customConfig;
			}
			set
			{
				this.config.customConfig = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("en"), Description("The language to be used if CKEDITOR.config.language is left empty and it's not possible to localize the editor to the user language.")]
		public string DefaultLanguage
		{
			get
			{
				return this.config.defaultLanguage;
			}
			set
			{
				this.config.defaultLanguage = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(DialogButtonsOrder), "OS"), Description("The guideline to follow when generating the dialog buttons. There are 3 possible options:\r\n'OS' - the buttons will be displayed in the default order of the user's OS;\r\n'ltr' - for Left-To-Right order;\r\n'rtl' - for Right-To-Left order.")]
		public DialogButtonsOrder DialogButtonsOrder
		{
			get
			{
				return this.config.dialog_buttonsOrder;
			}
			set
			{
				this.config.dialog_buttonsOrder = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Disables the built-in spell checker while typing natively available in the browser (currently Firefox and Safari only).\r\nEven if word suggestions will not appear in the CKEditor context menu, this feature is useful to help quickly identifying misspelled words.\r\nThis setting is currently compatible with Firefox only due to limitations in other browsers.")]
		public bool DisableNativeSpellChecker
		{
			get
			{
				return this.config.disableNativeSpellChecker;
			}
			set
			{
				this.config.disableNativeSpellChecker = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Disables the \"table tools\" offered natively by the browser (currently Firefox only) to make quick table editing operations, \r\nlike adding or deleting rows and columns.")]
		public bool DisableNativeTableHandles
		{
			get
			{
				return this.config.disableNativeTableHandles;
			}
			set
			{
				this.config.disableNativeTableHandles = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(false), Description("Disables the ability of resize objects (image and tables) in the editing area.")]
		public bool DisableObjectResizing
		{
			get
			{
				return this.config.disableObjectResizing;
			}
			set
			{
				this.config.disableObjectResizing = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(false), Description("Disables inline styling on read-only elements.")]
		public bool DisableReadonlyStyling
		{
			get
			{
				return this.config.disableReadonlyStyling;
			}
			set
			{
				this.config.disableReadonlyStyling = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">"), Description("Sets the doctype to be used when loading the editor content as HTML.")]
		public string DocType
		{
			get
			{
				return this.config.docType;
			}
			set
			{
				this.config.docType = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether to render or not the editing block area in the editor interface.")]
		public bool EditingBlock
		{
			get
			{
				return this.config.editingBlock;
			}
			set
			{
				this.config.editingBlock = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("The e-mail address anti-spam protection option. The protection will be applied when creating or modifying e-mail links through the editor interface.\r\nTwo methods of protection can be choosed: \r\nThe e-mail parts (name, domain and any other query string) are assembled into a function call pattern. Such function must be provided by \r\nthe developer in the pages that will use the contents. \r\nOnly the e-mail address is obfuscated into a special string that has no meaning for humans or spam bots, but which is properly rendered and accepted by the browser.\r\nBoth approaches require JavaScript to be enabled.")]
		public string EmailProtection
		{
			get
			{
				return this.config.emailProtection;
			}
			set
			{
				this.config.emailProtection = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Allow context-sensitive tab key behaviors, including the following scenarios: \r\nWhen selection is anchored inside table cells:\r\nIf TAB is pressed, select the contents of the \"next\" cell. If in the last cell in the table, add a new row to it and focus its first cell.\r\nIf SHIFT+TAB is pressed, select the contents of the \"previous\" cell. Do nothing when it's in the first cell.")]
		public bool EnableTabKeyTools
		{
			get
			{
				return this.config.enableTabKeyTools;
			}
			set
			{
				this.config.enableTabKeyTools = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(EnterMode), "P"), Description("Sets the behavior for the ENTER key. It also dictates other behaviour rules in the editor, like whether the <br> element is to be used as a paragraph separator when indenting text.\r\nThe allowed values are the following constants, and their relative behavior: \r\nCKEDITOR.ENTER_P (1): new <p> paragraphs are created;\r\nCKEDITOR.ENTER_BR (2): lines are broken with <br> elements;\r\nCKEDITOR.ENTER_DIV (3): new <div> blocks are created.\r\nNote: It's recommended to use the CKEDITOR.ENTER_P value because of its semantic value and correctness. The editor is optimized for this value.")]
		public EnterMode EnterMode
		{
			get
			{
				return this.config.enterMode;
			}
			set
			{
				this.config.enterMode = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(true), Description("Whether to use HTML entities in the output.")]
		public bool Entities
		{
			get
			{
				return this.config.entities;
			}
			set
			{
				this.config.entities = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("#39"), Description("An additional list of entities to be used. It's a string containing each entry separated by a comma.\r\nEntities names or number must be used, exclusing the \"&\" preffix and the \";\" termination.")]
		public string EntitiesAdditional
		{
			get
			{
				return this.config.entities_additional;
			}
			set
			{
				this.config.entities_additional = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(true), Description("Whether to convert some symbols, mathematical symbols, and Greek letters to HTML entities. This may be more relevant for users typing text written in Greek.\r\nThe list of entities can be found at the W3C HTML 4.01 Specification, section 24.3.1.")]
		public bool EntitiesGreek
		{
			get
			{
				return this.config.entities_greek;
			}
			set
			{
				this.config.entities_greek = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(true), Description("Whether to convert some Latin characters (Latin alphabet No. 1, ISO 8859-1) to HTML entities.\r\nThe list of entities can be found at the W3C HTML 4.01 Specification, section 24.2.1.")]
		public bool EntitiesLatin
		{
			get
			{
				return this.config.entities_latin;
			}
			set
			{
				this.config.entities_latin = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("List of additional plugins to be loaded. \r\nThis is a tool setting which makes it easier to add new plugins, whithout having to touch and possibly breaking the CKEDITOR.config.plugins setting.")]
		public string ExtraPlugins
		{
			get
			{
				return this.config.extraPlugins;
			}
			set
			{
				this.config.extraPlugins = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("The location of an external file browser, that should be launched when \"Browse Server\" button is pressed.\r\nIf configured, the \"Browse Server\" button will appear in Link, Image and Flash dialogs.")]
		public string FilebrowserBrowseUrl
		{
			get
			{
				return this.config.filebrowserBrowseUrl;
			}
			set
			{
				this.config.filebrowserBrowseUrl = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("The location of an external file browser, that should be launched when \"Browse Server\" button is pressed in the Flash dialog.\r\nIf not set, CKEditor will use CKEDITOR.config.filebrowserBrowseUrl.")]
		public string FilebrowserFlashBrowseUrl
		{
			get
			{
				return this.config.filebrowserFlashBrowseUrl;
			}
			set
			{
				this.config.filebrowserFlashBrowseUrl = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("The location of a script that handles file uploads in the Flash dialog. If not set, CKEditor will use CKEDITOR.config.filebrowserUploadUrl.")]
		public string FilebrowserFlashUploadUrl
		{
			get
			{
				return this.config.filebrowserFlashUploadUrl;
			}
			set
			{
				this.config.filebrowserFlashUploadUrl = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("The location of an external file browser, that should be launched when \"Browse Server\" button is pressed in the Link tab of Image dialog.\r\nIf not set, CKEditor will use CKEDITOR.config.filebrowserBrowseUrl.")]
		public string FilebrowserImageBrowseLinkUrl
		{
			get
			{
				return this.config.filebrowserImageBrowseLinkUrl;
			}
			set
			{
				this.config.filebrowserImageBrowseLinkUrl = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("The location of an external file browser, that should be launched when \"Browse Server\" button is pressed in the Image dialog.\r\nIf not set, CKEditor will use CKEDITOR.config.filebrowserBrowseUrl.")]
		public string FilebrowserImageBrowseUrl
		{
			get
			{
				return this.config.filebrowserImageBrowseUrl;
			}
			set
			{
				this.config.filebrowserImageBrowseUrl = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("The location of a script that handles file uploads in the Image dialog. If not set, CKEditor will use CKEDITOR.config.filebrowserUploadUrl.")]
		public string FilebrowserImageUploadUrl
		{
			get
			{
				return this.config.filebrowserImageUploadUrl;
			}
			set
			{
				this.config.filebrowserImageUploadUrl = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("The location of a script that handles file uploads. If set, the \"Upload\" tab will appear in \"Link\", \"Image\" and \"Flash\" dialogs.")]
		public string FilebrowserUploadUrl
		{
			get
			{
				return this.config.filebrowserUploadUrl;
			}
			set
			{
				this.config.filebrowserUploadUrl = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue("location=no,menubar=no,toolbar=no,dependent=yes,minimizable=no,modal=yes,alwaysRaised=yes,resizable=yes,scrollbars=yes"), Description("The \"features\" to use in the file browser popup window."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string FilebrowserWindowFeatures
		{
			get
			{
				return this.config.filebrowserWindowFeatures;
			}
			set
			{
				this.config.filebrowserWindowFeatures = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether a filler text (non-breaking space entity -  ) will be inserted into empty block elements in HTML output, \r\nthis is used to render block elements properly with line-height; When a function is instead specified, \r\nit'll be passed a CKEDITOR.htmlParser.element to decide whether adding the filler text by expecting a boolean return value.")]
		public bool FillEmptyBlocks
		{
			get
			{
				return this.config.fillEmptyBlocks;
			}
			set
			{
				this.config.fillEmptyBlocks = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("The text to be displayed in the Font combo is none of the available values matches the current cursor position or text selection.")]
		public string FontDefaultLabel
		{
			get
			{
				return this.config.font_defaultLabel;
			}
			set
			{
				this.config.font_defaultLabel = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("Arial/Arial, Helvetica, sans-serif;\r\nComic Sans MS/Comic Sans MS, cursive;\r\nCourier New/Courier New, Courier, monospace;\r\nGeorgia/Georgia, serif;\r\nLucida Sans Unicode/Lucida Sans Unicode, Lucida Grande, sans-serif;\r\nTahoma/Tahoma, Geneva, sans-serif;\r\nTimes New Roman/Times New Roman, Times, serif;\r\nTrebuchet MS/Trebuchet MS, Helvetica, sans-serif;\r\nVerdana/Verdana, Geneva, sans-serif"), Description("The list of fonts names to be displayed in the Font combo in the toolbar. Entries are separated by semi-colons (;),\r\nwhile it's possible to have more than one font for each entry, in the HTML way (separated by comma).\r\nA display name may be optionally defined by prefixing the entries with the name and the slash character."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string FontNames
		{
			get
			{
				return this.config.font_names;
			}
			set
			{
				this.config.font_names = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("The text to be displayed in the Font Size combo is none of the available values matches the current cursor position or text selection.")]
		public string FontSizeDefaultLabel
		{
			get
			{
				return this.config.fontSize_defaultLabel;
			}
			set
			{
				this.config.fontSize_defaultLabel = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("8/8px;9/9px;10/10px;11/11px;12/12px;14/14px;16/16px;18/18px;20/20px;22/22px;24/24px;26/26px;28/28px;36/36px;48/48px;72/72px"), Description("The list of fonts size to be displayed in the Font Size combo in the toolbar. Entries are separated by semi-colons (;).\r\nAny kind of \"CSS like\" size can be used, like \"12px\", \"2.3em\", \"130%\", \"larger\" or \"x-small\".\r\nA display name may be optionally defined by prefixing the entries with the name and the slash character."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string FontSizeSizes
		{
			get
			{
				return this.config.fontSize_sizes;
			}
			set
			{
				this.config.fontSize_sizes = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(false), Description("Force the respect of CKEDITOR.config.enterMode as line break regardless of the context, \r\nE.g. If CKEDITOR.config.enterMode is set to CKEDITOR.ENTER_P, press enter key inside a 'div' will create a new paragraph with 'p' instead of 'div'.")]
		public bool ForceEnterMode
		{
			get
			{
				return this.config.forceEnterMode;
			}
			set
			{
				this.config.forceEnterMode = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(false), Description("Whether to force all pasting operations to insert on plain text into the editor, loosing any formatting information possibly available in the source text.")]
		public bool ForcePasteAsPlainText
		{
			get
			{
				return this.config.forcePasteAsPlainText;
			}
			set
			{
				this.config.forcePasteAsPlainText = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(false), Description(" Whether to force using \"&\" instead of \"&amp;\" in elements attributes values,\r\nit's not recommended to change this setting for compliance with the W3C XHTML 1.0 standards (C.12, XHTML 1.0).")]
		public bool ForceSimpleAmpersand
		{
			get
			{
				return this.config.forceSimpleAmpersand;
			}
			set
			{
				this.config.forceSimpleAmpersand = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("p;h1;h2;h3;h4;h5;h6;pre;address;div"), Description(" A list of semi colon separated style names (by default tags) representing the style definition for each entry to be displayed\r\nin the Format combo in the toolbar. Each entry must have its relative definition configuration in a setting named\r\n\"format_(tagName)\". For example, the \"p\" entry has its definition taken from config.format_p.\r\n"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public string FormatTags
		{
			get
			{
				return this.config.format_tags;
			}
			set
			{
				this.config.format_tags = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(false), Description("Indicates whether the contents to be edited are being inputted as a full HTML page. \r\nA full page includes the <html>, <head> and <body> tags. \r\nThe final output will also reflect this setting, including the <body> contents only if this setting is disabled.")]
		public bool FullPage
		{
			get
			{
				return this.config.fullPage;
			}
			set
			{
				this.config.fullPage = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether escape HTML when editor update original input element.")]
		public bool HtmlEncodeOutput
		{
			get
			{
				return this.config.htmlEncodeOutput;
			}
			set
			{
				this.config.htmlEncodeOutput = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether the editor must output an empty value (\"\") if it's contents is made by an empty paragraph only.")]
		public bool IgnoreEmptyParagraph
		{
			get
			{
				return this.config.ignoreEmptyParagraph;
			}
			set
			{
				this.config.ignoreEmptyParagraph = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("Padding text to set off the image in preview area.")]
		public string ImagePreviewText
		{
			get
			{
				return this.config.image_previewText;
			}
			set
			{
				this.config.image_previewText = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether to remove links when emptying the link URL field in the image dialog.")]
		public bool ImageRemoveLinkByEmptyURL
		{
			get
			{
				return this.config.image_removeLinkByEmptyURL;
			}
			set
			{
				this.config.image_removeLinkByEmptyURL = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(Array), null), Description("List of classes to use for indenting the contents. If it's null, no classes will be used and instead the #indentUnit and #indentOffset properties will be used."), TypeConverter(typeof(StringArrayConverter)), PersistenceMode(PersistenceMode.Attribute)]
		public string[] IndentClasses
		{
			get
			{
				return this.config.indentClasses;
			}
			set
			{
				this.config.indentClasses = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(40), Description("Size of each indentation step")]
		public int IndentOffset
		{
			get
			{
				return this.config.indentOffset;
			}
			set
			{
				this.config.indentOffset = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue("px"), Description("Unit for the indentation style")]
		public string IndentUnit
		{
			get
			{
				return this.config.indentUnit;
			}
			set
			{
				this.config.indentUnit = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(""), Description("The user interface language localization to use. If empty, the editor automatically localize the editor to the user language,\r\nif supported, otherwise the CKEDITOR.config.defaultLanguage language is used.")]
		public string Language
		{
			get
			{
				return this.config.language;
			}
			set
			{
				this.config.language = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(false), Description("Whether to transform MS Word outline numbered headings into lists.")]
		public bool PasteFromWordNumberedHeadingToList
		{
			get
			{
				return this.config.pasteFromWordNumberedHeadingToList;
			}
			set
			{
				this.config.pasteFromWordNumberedHeadingToList = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(false), Description("Whether to prompt the user about the clean up of content being pasted from MS Word.")]
		public bool PasteFromWordPromptCleanup
		{
			get
			{
				return this.config.pasteFromWordPromptCleanup;
			}
			set
			{
				this.config.pasteFromWordPromptCleanup = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether to ignore all font related formatting styles, including: \r\nfont size;\r\nfont family;\r\nfont foreground/background color.")]
		public bool PasteFromWordRemoveFontStyles
		{
			get
			{
				return this.config.pasteFromWordRemoveFontStyles;
			}
			set
			{
				this.config.pasteFromWordRemoveFontStyles = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(false), Description("Whether to remove element styles that can't be managed with the editor. Note that this doesn't handle the font specific styles,\r\nwhich depends on the CKEDITOR.config.pasteFromWordRemoveFontStyles setting instead.")]
		public bool PasteFromWordRemoveStyles
		{
			get
			{
				return this.config.pasteFromWordRemoveStyles;
			}
			set
			{
				this.config.pasteFromWordRemoveStyles = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(Array), null), Description("List of regular expressions to be executed over the input HTML, indicating HTML source code that matched must not present in WYSIWYG mode for editing."), TypeConverter(typeof(StringArrayConverter)), PersistenceMode(PersistenceMode.Attribute)]
		public string[] ProtectedSource
		{
			get
			{
				return this.config.protectedSource;
			}
			set
			{
				this.config.protectedSource = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("The dialog contents to removed. It's a string composed by dialog name and tab name with a colon between them. \r\nSeparate each pair with semicolon (see example). Note: All names are case-sensitive. \r\nNote: Be cautious when specifying dialog tabs that are mandatory, like \"info\", \r\ndialog functionality might be broken because of this!")]
		public string RemoveDialogTabs
		{
			get
			{
				return this.config.removeDialogTabs;
			}
			set
			{
				this.config.removeDialogTabs = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("List of plugins that must not be loaded. This is a tool setting which makes it easier to avoid loading plugins definied \r\nin the CKEDITOR.config.plugins setting, whithout having to touch it and potentially breaking it.")]
		public string RemovePlugins
		{
			get
			{
				return this.config.removePlugins;
			}
			set
			{
				this.config.removePlugins = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(ResizeDir), "Both"), Description("The directions to which the editor resizing is enabled. Possible values are \"both\", \"vertical\" and \"horizontal\".")]
		public ResizeDir ResizeDir
		{
			get
			{
				return this.config.resize_dir;
			}
			set
			{
				this.config.resize_dir = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether to enable the resizing feature. If disabled the resize handler will not be visible.")]
		public bool ResizeEnabled
		{
			get
			{
				return this.config.resize_enabled;
			}
			set
			{
				this.config.resize_enabled = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(3000), Description("The maximum editor height, in pixels, when resizing it with the resize handle.")]
		public int ResizeMaxHeight
		{
			get
			{
				return this.config.resize_maxHeight;
			}
			set
			{
				this.config.resize_maxHeight = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(3000), Description("The maximum editor width, in pixels, when resizing it with the resize handle.")]
		public int ResizeMaxWidth
		{
			get
			{
				return this.config.resize_maxWidth;
			}
			set
			{
				this.config.resize_maxWidth = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(250), Description("The minimum editor height, in pixels, when resizing it with the resize handle. \r\nNote: It fallbacks to editor's actual height if that's smaller than the default value.")]
		public int ResizeMinHeight
		{
			get
			{
				return this.config.resize_minHeight;
			}
			set
			{
				this.config.resize_minHeight = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(750), Description("The minimum editor width, in pixels, when resizing it with the resize handle. \r\nNote: It fallbacks to editor's actual width if that's smaller than the default value.")]
		public int ResizeMinWidth
		{
			get
			{
				return this.config.resize_minWidth;
			}
			set
			{
				this.config.resize_minWidth = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(false), Description("If enabled (true), turns on SCAYT automatically after loading the editor.")]
		public bool ScaytAutoStartup
		{
			get
			{
				return this.config.scayt_autoStartup;
			}
			set
			{
				this.config.scayt_autoStartup = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("ID of bottom cntrol's shared")]
		public string SharedSpacesBottom
		{
			get
			{
				return this.config.sharedSpacesBottom;
			}
			set
			{
				this.config.sharedSpacesBottom = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(""), Description("ID of top cntrol's shared")]
		public string SharedSpacesTop
		{
			get
			{
				return this.config.sharedSpacesTop;
			}
			set
			{
				this.config.sharedSpacesTop = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(EnterMode), "P"), Description("Just like the CKEDITOR.config.enterMode setting, it defines the behavior for the SHIFT+ENTER key.\r\nThe allowed values are the following constants, and their relative behavior: \r\nCKEDITOR.ENTER_P (1): new <p> paragraphs are created;\r\nCKEDITOR.ENTER_BR (2): lines are broken with <br> elements;\r\nCKEDITOR.ENTER_DIV (3): new <div> blocks are created.")]
		public EnterMode ShiftEnterMode
		{
			get
			{
				return this.config.shiftEnterMode;
			}
			set
			{
				this.config.shiftEnterMode = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("kama"), Description("The skin to load. It may be the name of the skin folder inside the editor installation path, or the name and the path separated by a comma.")]
		public string Skin
		{
			get
			{
				return this.config.skin;
			}
			set
			{
				this.config.skin = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(StartupMode), "Wysiwyg"), Description("The mode to load at the editor startup. It depends on the plugins loaded. By default, the \"wysiwyg\" and \"source\" modes are available.")]
		public StartupMode StartupMode
		{
			get
			{
				return this.config.startupMode;
			}
			set
			{
				this.config.startupMode = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(false), Description("Whether to automaticaly enable the \"show block\" command when the editor loads. (StartupShowBlocks in FCKeditor)")]
		public bool StartupOutlineBlocks
		{
			get
			{
				return this.config.startupOutlineBlocks;
			}
			set
			{
				this.config.startupOutlineBlocks = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether to automatically enable the \"show borders\" command when the editor loads. (ShowBorders in FCKeditor)")]
		public bool StartupShowBorders
		{
			get
			{
				return this.config.startupShowBorders;
			}
			set
			{
				this.config.startupShowBorders = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue("default"), Description("The \"styles definition set\" to use in the editor. They will be used in the styles combo and the Style selector of the div container.\r\nThe styles may be defined in the page containing the editor, or can be loaded on demand from an external file. In the second case, \r\nif this setting contains only a name, the styles definition file will be loaded from the \"styles\" folder inside the styles plugin folder.\r\nOtherwise, this setting has the \"name:url\" syntax, making it possible to set the URL from which loading the styles file.\r\nPreviously this setting was available as config.stylesCombo_stylesSet"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), PersistenceMode(PersistenceMode.Attribute)]
		public string StylesSet
		{
			get
			{
				string retVal = string.Empty;
				string[] stylesSet = this.config.stylesSet;
				for (int i = 0; i < stylesSet.Length; i++)
				{
					string item = stylesSet[i];
					retVal = retVal + item + ",";
				}
				bool flag = retVal.EndsWith(",");
				if (flag)
				{
					retVal = retVal.Remove(retVal.Length - 1);
				}
				return retVal;
			}
			set
			{
				bool flag = value.Contains("{") && value.Contains("}");
				if (flag)
				{
					this.config.stylesSet = new string[]
					{
						value
					};
				}
				else
				{
					this.config.stylesSet = value.Replace("\t", string.Empty).Split(new char[]
					{
						',',
						'\n',
						'\r'
					}, StringSplitOptions.RemoveEmptyEntries);
				}
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(0), Description("Intructs the editor to add a number of spaces (&nbsp;) to the text when hitting the TAB key.\r\nIf set to zero, the TAB key will be used to move the cursor focus to the next element in the page, out of the editor focus.")]
		public int TabSpaces
		{
			get
			{
				return this.config.tabSpaces;
			}
			set
			{
				this.config.tabSpaces = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue("default"), Description("The templates definition set to use. It accepts a list of names separated by comma. It must match definitions loaded with the templates_files setting.")]
		public string Templates
		{
			get
			{
				return this.config.templates;
			}
			set
			{
				this.config.templates = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue("~/Scripts/ckeditor/plugins/templates/templates/default.js"), Description("The list of templates definition files to load."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), PersistenceMode(PersistenceMode.Attribute)]
		public string TemplatesFiles
		{
			get
			{
				string retVal = string.Empty;
				string[] templates_files = this.config.templates_files;
				for (int i = 0; i < templates_files.Length; i++)
				{
					string item = templates_files[i];
					retVal = retVal + item + ",";
				}
				bool flag = retVal.EndsWith(",");
				if (flag)
				{
					retVal = retVal.Remove(retVal.Length - 1);
				}
				return retVal;
			}
			set
			{
				this.config.templates_files = value.Replace("\t", string.Empty).Split(new char[]
				{
					',',
					'\n',
					'\r'
				}, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether the \"Replace actual contents\" checkbox is checked by default in the Templates dialog.")]
		public bool TemplatesReplaceContent
		{
			get
			{
				return this.config.templates_replaceContent;
			}
			set
			{
				this.config.templates_replaceContent = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("Full"), Description("The toolbox (alias toolbar) definition. It is a toolbar name or an array of toolbars (strips), each one being also an array, containing a list of UI items."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), PersistenceMode(PersistenceMode.Attribute)]
		public string Toolbar
		{
			get
			{
				bool flag = this.config.toolbar is string;
				string result;
				if (flag)
				{
					result = (string)this.config.toolbar;
				}
				else
				{
					string retVal = string.Empty;
					try
					{
						string[] retValTab = new string[((object[])this.config.toolbar).Length];
						object[] ob = (object[])this.config.toolbar;
						for (int i = 0; i < ob.Length; i++)
						{
							object item = ob[i];
							bool flag2 = item is string;
							if (flag2)
							{
								retValTab[i] = (string)item;
							}
							else
							{
								object[] itemTab = (object[])item;
								string concatValue = string.Empty;
								for (int j = 0; j < itemTab.Length; j++)
								{
									concatValue = concatValue + (string)itemTab[j] + "|";
								}
								bool flag3 = !string.IsNullOrEmpty(concatValue);
								if (flag3)
								{
									concatValue = concatValue.Remove(concatValue.Length - 1);
								}
								retValTab[i] = concatValue;
							}
						}
						string[] array = retValTab;
						for (int k = 0; k < array.Length; k++)
						{
							string item2 = array[k];
							retVal = retVal + item2 + "\r\n";
						}
						bool flag4 = retVal.EndsWith("\r\n");
						if (flag4)
						{
							retVal = retVal.Remove(retVal.Length - 2);
						}
					}
					catch
					{
					}
					result = retVal;
				}
				return result;
			}
			set
			{
				value = value.Trim();
				bool flag = value.StartsWith("[") && value.EndsWith("]");
				if (flag)
				{
					this.config.toolbar = value.Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
				}
				else
				{
					string[] valueTab = value.Split(new string[]
					{
						"\r\n"
					}, 2147483647, StringSplitOptions.RemoveEmptyEntries);
					bool flag2 = valueTab.Length == 1 && !valueTab[0].Contains("|");
					if (flag2)
					{
						this.config.toolbar = valueTab[0];
					}
					else
					{
						object[] retVal = new object[valueTab.Length];
						try
						{
							for (int i = 0; i < valueTab.Length; i++)
							{
								string[] item = valueTab[i].Split(new char[]
								{
									'|'
								});
								for (int j = 0; j < item.Length; j++)
								{
									item[j] = item[j].Trim();
								}
								bool flag3 = item.Length == 1 && item[0] == "/";
								if (flag3)
								{
									retVal[i] = item[0];
								}
								else
								{
									retVal[i] = item;
								}
							}
						}
						catch
						{
						}
						this.config.toolbar = retVal;
					}
				}
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("Bold|Italic|-|NumberedList|BulletedList|-|Link|Unlink|-|About"), Description("The toolbar definition. It is an array of toolbars (strips), each one being also an array, containing a list of UI items.\r\nNote that this setting is composed by \"toolbar_\" added by the toolbar name, which in this case is called \"Basic\".\r\nThis second part of the setting name can be anything. You must use this name in the CKEDITOR.config.toolbar setting,\r\nso you instruct the editor which toolbar_(name) setting to you."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), PersistenceMode(PersistenceMode.Attribute)]
		public string ToolbarBasic
		{
			get
			{
				string retVal = string.Empty;
				try
				{
					string[] retValTab = new string[this.config.toolbar_Basic.Length];
					object[] ob = this.config.toolbar_Basic;
					for (int i = 0; i < ob.Length; i++)
					{
						object item = ob[i];
						bool flag = item is string;
						if (flag)
						{
							retValTab[i] = (string)item;
						}
						else
						{
							object[] itemTab = (object[])item;
							string concatValue = string.Empty;
							for (int j = 0; j < itemTab.Length; j++)
							{
								concatValue = concatValue + (string)itemTab[j] + "|";
							}
							bool flag2 = !string.IsNullOrEmpty(concatValue);
							if (flag2)
							{
								concatValue = concatValue.Remove(concatValue.Length - 1);
							}
							retValTab[i] = concatValue;
						}
					}
					string[] array = retValTab;
					for (int k = 0; k < array.Length; k++)
					{
						string item2 = array[k];
						retVal = retVal + item2 + "\r\n";
					}
					bool flag3 = retVal.EndsWith("\r\n");
					if (flag3)
					{
						retVal = retVal.Remove(retVal.Length - 2);
					}
				}
				catch
				{
				}
				return retVal;
			}
			set
			{
				value = value.Trim();
				bool flag = value.StartsWith("[") && value.EndsWith("]");
				if (flag)
				{
					this.config.toolbar_Basic = new object[]
					{
						value.Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty)
					};
				}
				else
				{
					string[] valueTab = value.Split(new string[]
					{
						"\r\n"
					}, 2147483647, StringSplitOptions.RemoveEmptyEntries);
					object[] retVal = new object[valueTab.Length];
					try
					{
						for (int i = 0; i < valueTab.Length; i++)
						{
							string[] item = valueTab[i].Split(new char[]
							{
								'|'
							});
							for (int j = 0; j < item.Length; j++)
							{
								item[j] = item[j].Trim();
							}
							bool flag2 = item.Length == 1 && item[0] == "/";
							if (flag2)
							{
								retVal[i] = item[0];
							}
							else
							{
								retVal[i] = item;
							}
						}
					}
					catch
					{
					}
					this.config.toolbar_Basic = retVal;
				}
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue("Source|-|Save|NewPage|Preview|-|Templates\r\nCut|Copy|Paste|PasteText|PasteFromWord|-|Print|SpellChecker|Scayt\r\nUndo|Redo|-|Find|Replace|-|SelectAll|RemoveFormat\r\nForm|Checkbox|Radio|TextField|Textarea|Select|Button|ImageButton|HiddenField\r\n/\r\nBold|Italic|Underline|Strike|-|Subscript|Superscript\r\nNumberedList|BulletedList|-|Outdent|Indent|Blockquote|CreateDiv\r\nJustifyLeft|JustifyCenter|JustifyRight|JustifyBlock\r\nBidiLtr|BidiRtl\r\nLink|Unlink|Anchor\r\nImage|Flash|Table|HorizontalRule|Smiley|SpecialChar|PageBreak|Iframe\r\n/\r\nStyles|Format|Font|FontSize\r\nTextColor|BGColor\r\nMaximize|ShowBlocks|-|About"), Description("This is the default toolbar definition used by the editor. It contains all editor features."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), PersistenceMode(PersistenceMode.Attribute)]
		public string ToolbarFull
		{
			get
			{
				string retVal = string.Empty;
				try
				{
					string[] retValTab = new string[this.config.toolbar_Full.Length];
					object[] ob = this.config.toolbar_Full;
					for (int i = 0; i < ob.Length; i++)
					{
						object item = ob[i];
						bool flag = item is string;
						if (flag)
						{
							retValTab[i] = (string)item;
						}
						else
						{
							object[] itemTab = (object[])item;
							string concatValue = string.Empty;
							for (int j = 0; j < itemTab.Length; j++)
							{
								concatValue = concatValue + (string)itemTab[j] + "|";
							}
							bool flag2 = !string.IsNullOrEmpty(concatValue);
							if (flag2)
							{
								concatValue = concatValue.Remove(concatValue.Length - 1);
							}
							retValTab[i] = concatValue;
						}
					}
					string[] array = retValTab;
					for (int k = 0; k < array.Length; k++)
					{
						string item2 = array[k];
						retVal = retVal + item2 + "\r\n";
					}
					bool flag3 = retVal.EndsWith("\r\n");
					if (flag3)
					{
						retVal = retVal.Remove(retVal.Length - 2);
					}
				}
				catch
				{
				}
				return retVal;
			}
			set
			{
				value = value.Trim();
				bool flag = value.StartsWith("[") && value.EndsWith("]");
				if (flag)
				{
					this.config.toolbar_Full = new object[]
					{
						value.Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty)
					};
				}
				else
				{
					string[] valueTab = value.Split(new string[]
					{
						"\r\n"
					}, 2147483647, StringSplitOptions.RemoveEmptyEntries);
					object[] retVal = new object[valueTab.Length];
					try
					{
						for (int i = 0; i < valueTab.Length; i++)
						{
							string[] item = valueTab[i].Split(new char[]
							{
								'|'
							});
							for (int j = 0; j < item.Length; j++)
							{
								item[j] = item[j].Trim();
							}
							bool flag2 = item.Length == 1 && item[0] == "/";
							if (flag2)
							{
								retVal[i] = item[0];
							}
							else
							{
								retVal[i] = item;
							}
						}
					}
					catch
					{
					}
					this.config.toolbar_Full = retVal;
				}
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("This is the default toolbar definition used by the editor. It contains all editor features.")]
		public bool ToolbarCanCollapse
		{
			get
			{
				return this.config.toolbarCanCollapse;
			}
			set
			{
				this.config.toolbarCanCollapse = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(typeof(ToolbarLocation), "Top"), Description("The \"theme space\" to which rendering the toolbar. For the default theme, the recommended options are \"top\" and \"bottom\".")]
		public ToolbarLocation ToolbarLocation
		{
			get
			{
				return this.config.toolbarLocation;
			}
			set
			{
				this.config.toolbarLocation = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Whether the toolbar must start expanded when the editor is loaded.")]
		public bool ToolbarStartupExpanded
		{
			get
			{
				return this.config.toolbarStartupExpanded;
			}
			set
			{
				this.config.toolbarStartupExpanded = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(true), Description("Indicates that some of the editor features, like alignment and text direction, should used the \"computed value\"\r\nof the featureto indicate it's on/off state, instead of using the \"real value\".\r\nIf enabled, in a left to right written document, the \"Left Justify\" alignment button will show as active,\r\neven if the aligment style is not explicitly applied to the current paragraph in the editor.")]
		public bool UseComputedState
		{
			get
			{
				return this.config.useComputedState;
			}
			set
			{
				this.config.useComputedState = value;
			}
		}

		[Category("CKEditor Basic Settings"), DefaultValue(typeof(Color), "LightGray"), Description("Specifies the color of the user interface. Works only with the Kama skin."), TypeConverter(typeof(WebColorConverter))]
		public Color UIColor
		{
			get
			{
				bool flag = this.config.uiColor == "#D3D3D3";
				Color result;
				if (flag)
				{
					result = Color.FromName("LightGray");
				}
				else
				{
					result = ColorTranslator.FromHtml(this.config.uiColor);
				}
				return result;
			}
			set
			{
				this.config.uiColor = "#" + value.R.ToString("x2") + value.G.ToString("x2") + value.B.ToString("x2");
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(null), Description("To attach a function to CKEditor events.")]
		public List<object> CKEditorEventHandler
		{
			get
			{
				return this.config.CKEditorEventHandler;
			}
			set
			{
				this.config.CKEditorEventHandler = value;
			}
		}

		[Category("CKEditor Other Settings"), DefaultValue(null), Description("To attach a function to an event in a single editor instance")]
		public List<object> CKEditorInstanceEventHandler
		{
			get
			{
				return this.config.CKEditorInstanceEventHandler;
			}
			set
			{
				this.config.CKEditorInstanceEventHandler = value;
			}
		}

		private bool HasMsAjax
		{
			get
			{
				bool flag = !TimCKEditor._HasMsAjax.HasValue;
				if (flag)
				{
					TimCKEditor._HasMsAjax = new bool?(false);
					Assembly[] AppAssemblies = AppDomain.CurrentDomain.GetAssemblies();
					Assembly[] array = AppAssemblies;
					for (int i = 0; i < array.Length; i++)
					{
						Assembly asm = array[i];
						bool flag2 = asm.ManifestModule.Name == "System.Web.Extensions.dll";
						if (flag2)
						{
							try
							{
								TimCKEditor.updatePanel = asm.GetType("System.Web.UI.UpdatePanel");
								TimCKEditor.typScriptManager = asm.GetType("System.Web.UI.ScriptManager");
								bool flag3 = TimCKEditor.typScriptManager != null;
								if (flag3)
								{
									TimCKEditor._HasMsAjax = new bool?(true);
								}
							}
							catch
							{
							}
							break;
						}
					}
				}
				return TimCKEditor._HasMsAjax ?? false;
			}
		}

		private void RegisterClientScriptInclude(Type type, string key, string url)
		{
			bool hasMsAjax = this.HasMsAjax;
			if (hasMsAjax)
			{
				bool flag = TimCKEditor.mtdRegisterClientScriptInclude == null;
				if (flag)
				{
					TimCKEditor.mtdRegisterClientScriptInclude = TimCKEditor.typScriptManager.GetMethod("RegisterClientScriptInclude", new Type[]
					{
						typeof(Control),
						typeof(Type),
						typeof(string),
						typeof(string)
					});
				}
				TimCKEditor.mtdRegisterClientScriptInclude.Invoke(null, new object[]
				{
					this,
					type,
					key,
					url
				});
			}
			else
			{
				this.Page.ClientScript.RegisterClientScriptInclude(type, key, url);
			}
		}

		private void RegisterStartupScript(Type type, string key, string script, bool addScriptTags)
		{
			bool hasMsAjax = this.HasMsAjax;
			if (hasMsAjax)
			{
				bool flag = TimCKEditor.mtdRegisterStartupScript == null;
				if (flag)
				{
					TimCKEditor.mtdRegisterStartupScript = TimCKEditor.typScriptManager.GetMethod("RegisterStartupScript", new Type[]
					{
						typeof(Control),
						typeof(Type),
						typeof(string),
						typeof(string),
						typeof(bool)
					});
				}
				TimCKEditor.mtdRegisterStartupScript.Invoke(null, new object[]
				{
					this,
					type,
					key,
					script,
					addScriptTags
				});
			}
			else
			{
				this.Page.ClientScript.RegisterStartupScript(type, key, script, addScriptTags);
			}
		}

		private void RegisterOnSubmitStatement(Type type, string key, string script)
		{
			bool hasMsAjax = this.HasMsAjax;
			if (hasMsAjax)
			{
				bool flag = TimCKEditor.mtdRegisterOnSubmitStatement == null;
				if (flag)
				{
					TimCKEditor.mtdRegisterOnSubmitStatement = TimCKEditor.typScriptManager.GetMethod("RegisterOnSubmitStatement", new Type[]
					{
						typeof(Control),
						typeof(Type),
						typeof(string),
						typeof(string)
					});
				}
				TimCKEditor.mtdRegisterOnSubmitStatement.Invoke(null, new object[]
				{
					this,
					type,
					key,
					script
				});
			}
			else
			{
				this.Page.ClientScript.RegisterOnSubmitStatement(type, key, script);
			}
		}

		public TimCKEditor()
		{
			base.TextMode = TextBoxMode.MultiLine;
			this.config.defaultLanguage = "zh-cn";
			this.config.baseFloatZIndex = 600;
			this.config.removePlugins = "elementspath";
			this.config.resize_enabled = false;
			this.config.enterMode = EnterMode.DIV;
			this.config.font_names = "微软雅黑;宋体;Arial;Times New Roman;Verdana";
			this.config.font_defaultLabel = "微软雅黑";
			this.config.fontSize_sizes = "8/8px;9/9px;10/10px;11/11px;12/12px;14/14px;16/16px;18/18px;20/20px;22/22px;24/24px;26/26px;28/28px;36/36px;48/48px;72/72px";
			this.config.fontSize_defaultLabel = "9px";
			this.config.toolbar = new object[]
			{
				new object[]
				{
					"Bold",
					"Italic",
					"Underline",
					"Strike"
				},
				new object[]
				{
					"NumberedList",
					"BulletedList",
					"-",
					"Outdent",
					"Indent",
					"Blockquote",
					"CreateDiv"
				},
				new object[]
				{
					"JustifyLeft",
					"JustifyCenter",
					"JustifyRight",
					"JustifyBlock"
				},
				new object[]
				{
					"TextColor",
					"BGColor"
				}
			};
			this.ToolbarCanCollapse = true;
			this.ToolbarStartupExpanded = false;
		}

		public override void Focus()
		{
			base.Focus();
			this.config.startupFocus = true;
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.RegisterStartupScript(base.GetType(), "CKEDITOR_BASEPATH", string.Format("window.CKEDITOR_BASEPATH = '{0}/';\n", this.CKEditorJSFile.StartsWith("~") ? base.ResolveUrl(this.BasePath) : this.BasePath), true);
			this.RegisterStartupScript(base.GetType(), "ckeditor", "<script src=\"" + (this.CKEditorJSFile.StartsWith("~") ? base.ResolveUrl(this.CKEditorJSFile) : this.CKEditorJSFile) + this.timestamp + "\" type=\"text/javascript\"></script>", false);
			string scriptInit = string.Empty;
			string doPostBackScript = string.Empty;
			bool autoPostBack = this.AutoPostBack;
			if (autoPostBack)
			{
				doPostBackScript = string.Format("CKEDITOR.instances['{0}'].on('blur', function() {{if(this.checkDirty()){{javascript:setTimeout('__doPostBack(\\'{0}\\',\\'\\')',0); }}}});", this.ClientID);
			}
			scriptInit += "var CKEditor_Controls=[],CKEditor_Init=[];function CKEditor_TextBoxEncode(d,e){var f;if(typeof CKEDITOR=='undefined'||typeof CKEDITOR.instances[d]=='undefined'){f=document.getElementById(d);if(f)f.value=f.value.replace(/</g,'&lt;').replace(/>/g,'&gt;');}else{var g=CKEDITOR.instances[d];if(e&&(typeof Page_BlockSubmit=='undefined'||!Page_BlockSubmit)){g.destroy();f=document.getElementById(d);if(f)f.style.visibility='hidden';}else g.updateElement();}};(function(){if(typeof CKEDITOR!='undefined'){var d=document.getElementById('#this.ClientID#');if(d)d.style.visibility='hidden';}var e=function(){var f=CKEditor_Controls,g=CKEditor_Init,h=window.pageLoad,i=function(){for(var j=f.length;j--;){var k=document.getElementById(f[j]);if(k&&k.value&&(k.value.indexOf('<')==-1||k.value.indexOf('>')==-1))k.value=k.value.replace(/&lt;/g,'<').replace(/&gt;/g,'>').replace(/&amp;/g,'&');}if(typeof CKEDITOR!='undefined')for(var j=0;j<g.length;j++)g[j].call(this);};window.pageLoad=function(j,k){if(k.get_isPartialLoad())setTimeout(i,0);if(h&&typeof h=='function')h.call(this,j,k);};if(typeof Page_ClientValidate=='function'&&typeof CKEDITOR!='undefined')Page_ClientValidate=CKEDITOR.tools.override(Page_ClientValidate,function(j){return function(){for(var k in CKEDITOR.instances){if(document.getElementById(k))CKEDITOR.instances[k].updateElement();}return j.apply(this,arguments);};});setTimeout(i,0);};if(typeof Sys!='undefined'&&typeof Sys.Application!='undefined')Sys.Application.add_load(e);if(window.addEventListener)window.addEventListener('load',e,false);else if(window.attachEvent)window.attachEvent('onload',e);})();";
			scriptInit = scriptInit.Replace("#this.ClientID#", this.ClientID);
			this.RegisterStartupScript(base.GetType(), "CKEditorForNet", scriptInit, true);
			this.RegisterStartupScript(base.GetType(), this.ClientID + "_addControl", string.Format("CKEditor_Controls.push('{0}');\r\n", this.ClientID), true);
			string script = string.Empty;
			bool flag = this.config.CKEditorEventHandler != null;
			if (flag)
			{
				using (List<object>.Enumerator enumerator = this.config.CKEditorEventHandler.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object[] item = (object[])enumerator.Current;
						script += string.Format("CKEditor_Init.push(function(){{CKEDITOR.on('{0}',{1});}});\r\n", item[0], item[1]);
					}
				}
			}
			bool flag2 = this.config.protectedSource != null && this.config.protectedSource.Length != 0;
			if (flag2)
			{
				string proSour = string.Empty;
				string[] protectedSource = this.config.protectedSource;
				for (int i = 0; i < protectedSource.Length; i++)
				{
					string item2 = protectedSource[i];
					proSour = proSour + "\r\nckeditor.config.protectedSource.push( " + item2 + " );";
				}
				script += string.Format("CKEditor_Init.push(function(){{if(typeof CKEDITOR.instances['{0}']!='undefined' || !document.getElementById('{0}')) return;var ckeditor = CKEDITOR.replace('{0}',{1}); {2} {3}}});\r\n", new object[]
				{
					this.ClientID,
					this.prepareJSON(),
					proSour,
					doPostBackScript
				});
			}
			else
			{
				script += string.Format("CKEditor_Init.push(function(){{if(typeof CKEDITOR.instances['{0}']!='undefined' || !document.getElementById('{0}')) return;CKEDITOR.replace('{0}',{1}); {2}}});\r\n", this.ClientID, this.prepareJSON(), doPostBackScript);
			}
			bool isInUpdatePanel = false;
			Control con = this.Parent;
			bool flag3 = TimCKEditor.updatePanel != null;
			if (flag3)
			{
				while (con != null)
				{
					bool flag4 = con.GetType() == TimCKEditor.updatePanel;
					if (flag4)
					{
						isInUpdatePanel = true;
						break;
					}
					con = con.Parent;
				}
			}
			this.RegisterOnSubmitStatement(base.GetType(), "aspintegrator_Postback" + this.ClientID, string.Format("CKEditor_TextBoxEncode('{0}', {1}); ", this.ClientID, isInUpdatePanel ? 1 : 0));
			this.RegisterStartupScript(base.GetType(), "aspintegratorInitial_" + this.ClientID, script, true);
			bool flag5 = this.isChanged;
			if (flag5)
			{
				this.OnTextChanged(e);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			bool flag = this.ViewState["CKEditorConfig"] != null;
			if (flag)
			{
				this.config = (CKEditorConfig)this.ViewState["CKEditorConfig"];
			}
			else
			{
				this.config = new CKEditorConfig(this.BasePath.StartsWith("~") ? base.ResolveUrl(this.BasePath) : this.BasePath);
			}
		}

		private string prepareJSON()
		{
			return JSONSerializer.ToJavaScriptObjectNotation(this.config);
		}

		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool htmlEncodeOutput = this.config.htmlEncodeOutput;
			bool result;
			if (htmlEncodeOutput)
			{
				string postedValue = HttpUtility.HtmlDecode(postCollection[postDataKey]);
				bool flag = this.Text != postedValue;
				if (flag)
				{
					this.isChanged = true;
					this.Text = postedValue;
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
		}
	}
}

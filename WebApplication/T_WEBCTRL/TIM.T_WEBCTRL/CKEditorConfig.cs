using System;
using System.Collections;
using System.Collections.Generic;

namespace TIM.T_WEBCTRL
{
	[Serializable]
	public class CKEditorConfig
	{
		public const int CKEDITOR_CTRL = 1000;

		public const int CKEDITOR_SHIFT = 2000;

		public const int CKEDITOR_ALT = 4000;

		public static CKEditorConfig GlobalConfig;

		private string _sharedSpacesBottom;

		private string _sharedSpacesTop;

		[CKSerializable]
		public int autoGrow_bottomSpace
		{
			get;
			set;
		}

		[CKSerializable]
		public int autoGrow_maxHeight
		{
			get;
			set;
		}

		[CKSerializable]
		public int autoGrow_minHeight
		{
			get;
			set;
		}

		[CKSerializable]
		public bool autoGrow_onStartup
		{
			get;
			set;
		}

		[CKSerializable]
		public bool autoParagraph
		{
			get;
			set;
		}

		[CKSerializable]
		public bool autoUpdateElement
		{
			get;
			set;
		}

		[CKSerializable]
		public int baseFloatZIndex
		{
			get;
			set;
		}

		[CKSerializable]
		public string baseHref
		{
			get;
			set;
		}

		[CKSerializable]
		public bool basicEntities
		{
			get;
			set;
		}

		[CKSerializable]
		public int[] blockedKeystrokes
		{
			get;
			set;
		}

		[CKSerializable]
		public string bodyClass
		{
			get;
			set;
		}

		[CKSerializable]
		public string bodyId
		{
			get;
			set;
		}

		[CKSerializable]
		public bool browserContextMenuOnCtrl
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object colorButton_backStyle
		{
			get;
			set;
		}

		[CKSerializable]
		public string colorButton_colors
		{
			get;
			set;
		}

		[CKSerializable]
		public bool colorButton_enableMore
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object colorButton_foreStyle
		{
			get;
			set;
		}

		public string[] contentsCss
		{
			get;
			set;
		}

		[CKSerializable(Name = "contentsCss", IsObject = true)]
		private string[] contentsCssSer
		{
			get
			{
				List<string> retVal = new List<string>();
				this.ResolveParameters(this.contentsCss, retVal, true);
				return retVal.ToArray();
			}
		}

		public contentsLangDirections contentsLangDirection
		{
			get;
			set;
		}

		[CKSerializable(Name = "contentsLangDirection")]
		private string contentsLangDirectionSer
		{
			get
			{
				return this.contentsLangDirection.ToString().ToLower();
			}
		}

		[CKSerializable]
		public string contentsLanguage
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object coreStyles_bold
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object coreStyles_italic
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object coreStyles_strike
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object coreStyles_subscript
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object coreStyles_superscript
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object coreStyles_underline
		{
			get;
			set;
		}

		[CKSerializable]
		public string customConfig
		{
			get;
			set;
		}

		[CKSerializable]
		public string defaultLanguage
		{
			get;
			set;
		}

		[CKSerializable]
		public string devtools_styles
		{
			get;
			set;
		}

		[CKSerializable]
		public string dialog_backgroundCoverColor
		{
			get;
			set;
		}

		[CKSerializable]
		public double dialog_backgroundCoverOpacity
		{
			get;
			set;
		}

		public DialogButtonsOrder dialog_buttonsOrder
		{
			get;
			set;
		}

		[CKSerializable(Name = "dialog_buttonsOrder")]
		private string dialog_buttonsOrderSer
		{
			get
			{
				return this.dialog_buttonsOrder.ToString().ToLower();
			}
		}

		[CKSerializable]
		public int dialog_magnetDistance
		{
			get;
			set;
		}

		[CKSerializable]
		public bool dialog_startupFocusTab
		{
			get;
			set;
		}

		[CKSerializable]
		public bool disableNativeSpellChecker
		{
			get;
			set;
		}

		[CKSerializable]
		public bool disableNativeTableHandles
		{
			get;
			set;
		}

		[CKSerializable]
		public bool disableObjectResizing
		{
			get;
			set;
		}

		[CKSerializable]
		public bool disableReadonlyStyling
		{
			get;
			set;
		}

		[CKSerializable]
		public string docType
		{
			get;
			set;
		}

		[CKSerializable]
		public bool editingBlock
		{
			get;
			set;
		}

		[CKSerializable]
		public string emailProtection
		{
			get;
			set;
		}

		[CKSerializable]
		public bool enableTabKeyTools
		{
			get;
			set;
		}

		public EnterMode enterMode
		{
			get;
			set;
		}

		[CKSerializable(Name = "enterMode", IsObject = true)]
		private int enterModeSer
		{
			get
			{
				return (int)this.enterMode;
			}
		}

		[CKSerializable]
		public bool entities
		{
			get;
			set;
		}

		[CKSerializable]
		public string entities_additional
		{
			get;
			set;
		}

		[CKSerializable]
		public bool entities_greek
		{
			get;
			set;
		}

		[CKSerializable]
		public bool entities_latin
		{
			get;
			set;
		}

		[CKSerializable]
		public string entities_processNumerical
		{
			get;
			set;
		}

		[CKSerializable]
		public string extraPlugins
		{
			get;
			set;
		}

		[CKSerializable]
		public string filebrowserBrowseUrl
		{
			get;
			set;
		}

		[CKSerializable]
		public string filebrowserFlashBrowseUrl
		{
			get;
			set;
		}

		[CKSerializable]
		public string filebrowserFlashUploadUrl
		{
			get;
			set;
		}

		[CKSerializable]
		public string filebrowserImageBrowseLinkUrl
		{
			get;
			set;
		}

		[CKSerializable]
		public string filebrowserImageBrowseUrl
		{
			get;
			set;
		}

		[CKSerializable]
		public string filebrowserImageUploadUrl
		{
			get;
			set;
		}

		[CKSerializable]
		public string filebrowserUploadUrl
		{
			get;
			set;
		}

		[CKSerializable]
		public string filebrowserWindowFeatures
		{
			get;
			set;
		}

		[CKSerializable]
		public bool fillEmptyBlocks
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object find_highlight
		{
			get;
			set;
		}

		[CKSerializable]
		public string font_defaultLabel
		{
			get;
			set;
		}

		[CKSerializable(RemoveEnters = true)]
		public string font_names
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object font_style
		{
			get;
			set;
		}

		[CKSerializable]
		public string fontSize_defaultLabel
		{
			get;
			set;
		}

		[CKSerializable]
		public string fontSize_sizes
		{
			get;
			set;
		}

		[CKSerializable]
		public object fontSize_style
		{
			get;
			set;
		}

		[CKSerializable]
		public bool forceEnterMode
		{
			get;
			set;
		}

		[CKSerializable]
		public bool forcePasteAsPlainText
		{
			get;
			set;
		}

		[CKSerializable]
		public bool forceSimpleAmpersand
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_address
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_div
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_h1
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_h2
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_h3
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_h4
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_h5
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_h6
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_p
		{
			get;
			set;
		}

		[CKSerializable(IsObject = true)]
		public object format_pre
		{
			get;
			set;
		}

		[CKSerializable]
		public string format_tags
		{
			get;
			set;
		}

		[CKSerializable]
		public bool fullPage
		{
			get;
			set;
		}

		[CKSerializable]
		public string height
		{
			get;
			set;
		}

		[CKSerializable(ForceAddToJSON = true)]
		public bool htmlEncodeOutput
		{
			get;
			set;
		}

		[CKSerializable]
		public bool ignoreEmptyParagraph
		{
			get;
			set;
		}

		[CKSerializable]
		public string image_previewText
		{
			get;
			set;
		}

		[CKSerializable]
		public bool image_removeLinkByEmptyURL
		{
			get;
			set;
		}

		public string[] indentClasses
		{
			get;
			set;
		}

		[CKSerializable(Name = "indentClasses", IsObject = true)]
		private string[] indentClassesSer
		{
			get
			{
				List<string> retVal = new List<string>();
				this.ResolveParameters(this.indentClasses, retVal, true);
				return retVal.ToArray();
			}
		}

		[CKSerializable]
		public int indentOffset
		{
			get;
			set;
		}

		[CKSerializable]
		public string indentUnit
		{
			get;
			set;
		}

		[CKSerializable]
		public bool jqueryOverrideVal
		{
			get;
			set;
		}

		[CKSerializable]
		public object[] justifyClasses
		{
			get;
			set;
		}

		[CKSerializable]
		public object[] keystrokes
		{
			get;
			set;
		}

		[CKSerializable]
		public string language
		{
			get;
			set;
		}

		[CKSerializable]
		public string menu_groups
		{
			get;
			set;
		}

		[CKSerializable]
		public string newpage_html
		{
			get;
			set;
		}

		[CKSerializable]
		public string pasteFromWordCleanupFile
		{
			get;
			set;
		}

		[CKSerializable]
		public bool pasteFromWordNumberedHeadingToList
		{
			get;
			set;
		}

		[CKSerializable]
		public bool pasteFromWordPromptCleanup
		{
			get;
			set;
		}

		[CKSerializable]
		public bool pasteFromWordRemoveFontStyles
		{
			get;
			set;
		}

		[CKSerializable]
		public bool pasteFromWordRemoveStyles
		{
			get;
			set;
		}

		public string[] protectedSource
		{
			get;
			set;
		}

		[CKSerializable]
		public bool readOnly
		{
			get;
			set;
		}

		[CKSerializable]
		public string removeDialogTabs
		{
			get;
			set;
		}

		[CKSerializable]
		public string removeFormatAttributes
		{
			get;
			set;
		}

		[CKSerializable]
		public string removeFormatTags
		{
			get;
			set;
		}

		[CKSerializable]
		public string removePlugins
		{
			get;
			set;
		}

		public ResizeDir resize_dir
		{
			get;
			set;
		}

		[CKSerializable(Name = "resize_dir")]
		private string resize_dirSer
		{
			get
			{
				return this.resize_dir.ToString().ToLower();
			}
		}

		[CKSerializable]
		public bool resize_enabled
		{
			get;
			set;
		}

		[CKSerializable]
		public int resize_maxHeight
		{
			get;
			set;
		}

		[CKSerializable]
		public int resize_maxWidth
		{
			get;
			set;
		}

		[CKSerializable]
		public int resize_minHeight
		{
			get;
			set;
		}

		[CKSerializable]
		public int resize_minWidth
		{
			get;
			set;
		}

		[CKSerializable]
		public bool scayt_autoStartup
		{
			get;
			set;
		}

		public ScaytContextCommands scayt_contextCommands
		{
			get;
			set;
		}

		[CKSerializable(Name = "scayt_contextCommands")]
		private string scayt_contextCommandsSer
		{
			get
			{
				return this.scayt_contextCommands.ToString().ToLower().Replace(", ", "|").Replace(",", "|");
			}
		}

		public ScaytContextMenuItemsOrder scayt_contextMenuItemsOrder
		{
			get;
			set;
		}

		[CKSerializable(Name = "scayt_contextMenuItemsOrder")]
		private string scayt_contextMenuItemsOrderSer
		{
			get
			{
				return this.scayt_contextMenuItemsOrder.ToString().ToLower().Replace(", ", "|").Replace(",", "|");
			}
		}

		[CKSerializable]
		public string scayt_customDictionaryIds
		{
			get;
			set;
		}

		[CKSerializable]
		public string scayt_customerid
		{
			get;
			set;
		}

		[CKSerializable]
		public int scayt_maxSuggestions
		{
			get;
			set;
		}

		public ScaytMoreSuggestions scayt_moreSuggestions
		{
			get;
			set;
		}

		[CKSerializable(Name = "scayt_moreSuggestions")]
		private string scayt_moreSuggestionsSer
		{
			get
			{
				return this.scayt_moreSuggestions.ToString().ToLower();
			}
		}

		[CKSerializable]
		public string scayt_sLang
		{
			get;
			set;
		}

		[CKSerializable]
		public string scayt_srcUrl
		{
			get;
			set;
		}

		[CKSerializable]
		public string scayt_uiTabs
		{
			get;
			set;
		}

		[CKSerializable]
		public string scayt_userDictionaryName
		{
			get;
			set;
		}

		public string sharedSpacesBottom
		{
			get
			{
				return this._sharedSpacesBottom;
			}
			set
			{
				this._sharedSpacesBottom = value;
				bool flag = !string.IsNullOrEmpty(this._sharedSpacesBottom) && !this.removePlugins.Contains("resize");
				if (flag)
				{
					this.removePlugins = (string.IsNullOrEmpty(this.removePlugins) ? "resize" : (this.removePlugins + ",resize"));
				}
			}
		}

		public string sharedSpacesTop
		{
			get
			{
				return this._sharedSpacesTop;
			}
			set
			{
				this._sharedSpacesTop = value;
				bool flag = !string.IsNullOrEmpty(this._sharedSpacesTop) && !this.removePlugins.Contains("maximize");
				if (flag)
				{
					this.removePlugins = (string.IsNullOrEmpty(this.removePlugins) ? "maximize" : (this.removePlugins + ",maximize"));
				}
			}
		}

		[CKSerializable(IsObject = true)]
		private string sharedSpaces
		{
			get
			{
				string retVal = string.Empty;
				bool flag = !string.IsNullOrEmpty(this.sharedSpacesBottom) || !string.IsNullOrEmpty(this.sharedSpacesTop);
				if (flag)
				{
					retVal += "{";
					bool flag2 = !string.IsNullOrEmpty(this.sharedSpacesTop);
					if (flag2)
					{
						retVal = retVal + "top:'" + this.sharedSpacesTop + "'";
					}
					bool flag3 = !string.IsNullOrEmpty(this.sharedSpacesBottom) && !string.IsNullOrEmpty(this.sharedSpacesTop);
					if (flag3)
					{
						retVal += ",";
					}
					bool flag4 = !string.IsNullOrEmpty(this.sharedSpacesBottom);
					if (flag4)
					{
						retVal = retVal + "bottom:'" + this.sharedSpacesBottom + "'";
					}
					retVal += "}";
				}
				return retVal;
			}
		}

		public EnterMode shiftEnterMode
		{
			get;
			set;
		}

		[CKSerializable(Name = "shiftEnterMode")]
		private int shiftEnterModeSer
		{
			get
			{
				return (int)this.shiftEnterMode;
			}
		}

		[CKSerializable]
		public string skin
		{
			get;
			set;
		}

		[CKSerializable]
		public int smiley_columns
		{
			get;
			set;
		}

		[CKSerializable]
		public string[] smiley_descriptions
		{
			get;
			set;
		}

		[CKSerializable]
		public string[] smiley_images
		{
			get;
			set;
		}

		[CKSerializable]
		public string smiley_path
		{
			get;
			set;
		}

		[CKSerializable]
		public string[] specialChars
		{
			get;
			set;
		}

		[CKSerializable]
		public bool startupFocus
		{
			get;
			set;
		}

		public StartupMode startupMode
		{
			get;
			set;
		}

		[CKSerializable(Name = "startupMode")]
		private string startupModeSer
		{
			get
			{
				return this.startupMode.ToString().ToLower();
			}
		}

		[CKSerializable]
		public bool startupOutlineBlocks
		{
			get;
			set;
		}

		[CKSerializable]
		public bool startupShowBorders
		{
			get;
			set;
		}

		[CKSerializable]
		public string stylesheetParser_skipSelectors
		{
			get;
			set;
		}

		[CKSerializable]
		public string stylesheetParser_validSelectors
		{
			get;
			set;
		}

		public string[] stylesSet
		{
			get;
			set;
		}

		[CKSerializable(Name = "stylesSet", IsObject = true)]
		private string[] stylesSetSer
		{
			get
			{
				bool flag = this.stylesSet != null && this.stylesSet.Length == 1 && this.stylesSet[0].Contains("{") && this.stylesSet[0].Contains("}");
				string[] result;
				if (flag)
				{
					this.stylesSet[0] = this.stylesSet[0].Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
					result = this.stylesSet;
				}
				else
				{
					List<string> retVal = new List<string>();
					this.ResolveParameters(this.stylesSet, retVal, true);
					result = retVal.ToArray();
				}
				return result;
			}
		}

		[CKSerializable]
		public int tabIndex
		{
			get;
			set;
		}

		[CKSerializable]
		public int tabSpaces
		{
			get;
			set;
		}

		[CKSerializable]
		public string templates
		{
			get;
			set;
		}

		public string[] templates_files
		{
			get;
			set;
		}

		[CKSerializable(Name = "templates_files", IsObject = true)]
		private string[] templates_filesSer
		{
			get
			{
				List<string> retVal = new List<string>();
				this.ResolveParameters(this.templates_files, retVal, true);
				return retVal.ToArray();
			}
		}

		[CKSerializable]
		public bool templates_replaceContent
		{
			get;
			set;
		}

		[CKSerializable]
		public string theme
		{
			get;
			set;
		}

		[CKSerializable]
		public object toolbar
		{
			get;
			set;
		}

		[CKSerializable]
		public object[] toolbar_Basic
		{
			get;
			set;
		}

		[CKSerializable]
		public object[] toolbar_Full
		{
			get;
			set;
		}

		[CKSerializable]
		public bool toolbarCanCollapse
		{
			get;
			set;
		}

		[CKSerializable]
		public bool toolbarGroupCycling
		{
			get;
			set;
		}

		public ToolbarLocation toolbarLocation
		{
			get;
			set;
		}

		[CKSerializable(Name = "toolbarLocation")]
		private string toolbarLocationSer
		{
			get
			{
				return this.toolbarLocation.ToString().ToLower();
			}
		}

		[CKSerializable]
		public bool toolbarStartupExpanded
		{
			get;
			set;
		}

		[CKSerializable]
		public string uiColor
		{
			get;
			set;
		}

		[CKSerializable]
		public bool useComputedState
		{
			get;
			set;
		}

		[CKSerializable]
		public string width
		{
			get;
			set;
		}

		public List<object> CKEditorEventHandler
		{
			get;
			set;
		}

		private object[] CKEditorEventHandlerSer
		{
			get
			{
				object[] retVal = new object[0];
				bool flag = this.CKEditorEventHandler != null;
				if (flag)
				{
					retVal = this.CKEditorEventHandler.ToArray();
					object[] array = retVal;
					for (int i = 0; i < array.Length; i++)
					{
						object item = array[i];
						object[] stringArray = (object[])item;
						bool flag2 = !((string)stringArray[0]).StartsWith("'") && !((string)stringArray[0]).EndsWith("'");
						if (flag2)
						{
							stringArray[0] = "'" + stringArray[0] + "'";
						}
					}
				}
				return retVal;
			}
		}

		public List<object> CKEditorInstanceEventHandler
		{
			get;
			set;
		}

		[CKSerializable(Name = "on", IsObject = true)]
		private object[] CKEditorInstanceEventHandlerSer
		{
			get
			{
				object[] retVal = new object[0];
				bool flag = this.CKEditorInstanceEventHandler != null;
				if (flag)
				{
					retVal = this.CKEditorInstanceEventHandler.ToArray();
					object[] array = retVal;
					for (int i = 0; i < array.Length; i++)
					{
						object item = array[i];
						object[] stringArray = (object[])item;
						bool flag2 = !((string)stringArray[0]).StartsWith("'") && !((string)stringArray[0]).EndsWith("'");
						if (flag2)
						{
							stringArray[0] = "'" + stringArray[0] + "'";
						}
					}
				}
				return retVal;
			}
		}

		public Hashtable ExtraOptions
		{
			get;
			set;
		}

		[CKSerializable(Name = "ExtraOptions")]
		private object[] ExtraOptionsSer
		{
			get
			{
				object[] retVal = new object[this.ExtraOptions.Keys.Count];
				int i = 0;
				foreach (object item in this.ExtraOptions.Keys)
				{
					retVal[i++] = new object[]
					{
						item,
						this.ExtraOptions[item]
					};
				}
				return retVal;
			}
		}

		static CKEditorConfig()
		{
			CKEditorConfig.GlobalConfig = new CKEditorConfig
			{
				autoGrow_bottomSpace = 0,
				autoGrow_maxHeight = 0,
				autoGrow_minHeight = 200,
				autoGrow_onStartup = false,
				autoParagraph = true,
				autoUpdateElement = true,
				baseFloatZIndex = 10000,
				baseHref = string.Empty,
				basicEntities = true,
				blockedKeystrokes = new int[]
				{
					1066,
					1073,
					1085
				},
				bodyClass = string.Empty,
				bodyId = string.Empty,
				browserContextMenuOnCtrl = true,
				colorButton_backStyle = "{ element : 'span', styles : { 'background-color' : '#(color)' } }",
				colorButton_colors = "000,800000,8B4513,2F4F4F,008080,000080,4B0082,696969,B22222,A52A2A,DAA520,006400,40E0D0,0000CD,800080,808080,F00,FF8C00,FFD700,008000,0FF,00F,EE82EE,A9A9A9,FFA07A,FFA500,FFFF00,00FF00,AFEEEE,ADD8E6,DDA0DD,D3D3D3,FFF0F5,FAEBD7,FFFFE0,F0FFF0,F0FFFF,F0F8FF,E6E6FA,FFF",
				colorButton_enableMore = true,
				colorButton_foreStyle = "{ element : 'span',\r\nstyles : { 'color' : '#(color)' },\r\noverrides : [ { element : 'font', attributes : { 'color' : null } } ] }",
				contentsCss = new string[]
				{
					"~/contents.css"
				},
				contentsLangDirection = contentsLangDirections.Ui,
				contentsLanguage = string.Empty,
				coreStyles_bold = "{ element : 'strong', overrides : 'b' }",
				coreStyles_italic = "{ element : 'em', overrides : 'i' }",
				coreStyles_strike = "{ element : 'strike' }",
				coreStyles_subscript = "{ element : 'sub' }",
				coreStyles_superscript = "{ element : 'sup' }",
				coreStyles_underline = "{ element : 'u' }",
				customConfig = "config.js",
				defaultLanguage = "en",
				devtools_styles = "",
				dialog_backgroundCoverColor = "white",
				dialog_backgroundCoverOpacity = 0.5,
				dialog_buttonsOrder = DialogButtonsOrder.OS,
				dialog_magnetDistance = 20,
				dialog_startupFocusTab = false,
				disableNativeSpellChecker = true,
				disableNativeTableHandles = true,
				disableObjectResizing = false,
				disableReadonlyStyling = false,
				docType = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">",
				editingBlock = true,
				emailProtection = string.Empty,
				enableTabKeyTools = true,
				enterMode = EnterMode.P,
				entities = true,
				entities_additional = "#39",
				entities_greek = true,
				entities_latin = true,
				entities_processNumerical = false.ToString(),
				extraPlugins = string.Empty,
				filebrowserBrowseUrl = string.Empty,
				filebrowserFlashBrowseUrl = string.Empty,
				filebrowserFlashUploadUrl = string.Empty,
				filebrowserImageBrowseLinkUrl = string.Empty,
				filebrowserImageBrowseUrl = string.Empty,
				filebrowserImageUploadUrl = string.Empty,
				filebrowserUploadUrl = string.Empty,
				filebrowserWindowFeatures = "location=no,menubar=no,toolbar=no,dependent=yes,minimizable=no,modal=yes,alwaysRaised=yes,resizable=yes,scrollbars=yes",
				fillEmptyBlocks = true,
				find_highlight = "{ element : 'span', styles : { 'background-color' : '#004', 'color' : '#fff' } }",
				font_defaultLabel = string.Empty,
				font_names = "Arial/Arial, Helvetica, sans-serif;\r\nComic Sans MS/Comic Sans MS, cursive;\r\nCourier New/Courier New, Courier, monospace;\r\nGeorgia/Georgia, serif;\r\nLucida Sans Unicode/Lucida Sans Unicode, Lucida Grande, sans-serif;\r\nTahoma/Tahoma, Geneva, sans-serif;\r\nTimes New Roman/Times New Roman, Times, serif;\r\nTrebuchet MS/Trebuchet MS, Helvetica, sans-serif;\r\nVerdana/Verdana, Geneva, sans-serif",
				font_style = "{ element : 'span',\r\nstyles : { 'font-family' : '#(family)' },\r\noverrides : [ { element : 'font', attributes : { 'face' : null } } ] }",
				fontSize_defaultLabel = string.Empty,
				fontSize_sizes = "8/8px;9/9px;10/10px;11/11px;12/12px;14/14px;16/16px;18/18px;20/20px;22/22px;24/24px;26/26px;28/28px;36/36px;48/48px;72/72px",
				fontSize_style = "{ element : 'span',\r\nstyles : { 'font-size' : '#(size)' },\r\noverrides : [ { element : 'font', attributes : { 'size' : null } } ] }",
				forceEnterMode = false,
				forcePasteAsPlainText = false,
				forceSimpleAmpersand = false,
				format_address = "{ element : 'address' }",
				format_div = "{ element : 'div' }",
				format_h1 = "{ element : 'h1' }",
				format_h2 = "{ element : 'h2' }",
				format_h3 = "{ element : 'h3' }",
				format_h4 = "{ element : 'h4' }",
				format_h5 = "{ element : 'h5' }",
				format_h6 = "{ element : 'h6' }",
				format_p = "{ element : 'p' }",
				format_pre = "{ element : 'pre' }",
				format_tags = "p;h1;h2;h3;h4;h5;h6;pre;address;div",
				fullPage = false,
				height = "200",
				htmlEncodeOutput = true,
				ignoreEmptyParagraph = true,
				image_previewText = string.Empty,
				image_removeLinkByEmptyURL = true,
				indentClasses = new string[0],
				indentOffset = 40,
				indentUnit = "px",
				jqueryOverrideVal = true,
				justifyClasses = null,
				keystrokes = new object[]
				{
					new object[]
					{
						4121,
						"toolbarFocus"
					},
					new object[]
					{
						4122,
						"elementsPathFocus"
					},
					new object[]
					{
						2121,
						"contextMenu"
					},
					new object[]
					{
						1090,
						"undo"
					},
					new object[]
					{
						1089,
						"redo"
					},
					new object[]
					{
						3090,
						"redo"
					},
					new object[]
					{
						1076,
						"link"
					},
					new object[]
					{
						1066,
						"bold"
					},
					new object[]
					{
						1073,
						"italic"
					},
					new object[]
					{
						1085,
						"underline"
					},
					new object[]
					{
						4109,
						"toolbarCollapse"
					}
				},
				language = string.Empty,
				menu_groups = "clipboard,form,tablecell,tablecellproperties,tablerow,tablecolumn,table,anchor,link,image,flash,checkbox,radio,textfield,hiddenfield,imagebutton,button,select,textarea",
				newpage_html = string.Empty,
				pasteFromWordCleanupFile = "default",
				pasteFromWordNumberedHeadingToList = false,
				pasteFromWordPromptCleanup = false,
				pasteFromWordRemoveFontStyles = true,
				pasteFromWordRemoveStyles = false,
				protectedSource = new string[0],
				readOnly = false,
				removeDialogTabs = string.Empty,
				removeFormatAttributes = "class,style,lang,width,height,align,hspace,valign",
				removeFormatTags = "b,big,code,del,dfn,em,font,i,ins,kbd,q,samp,small,span,strike,strong,sub,sup,tt,u,var",
				removePlugins = string.Empty,
				resize_dir = ResizeDir.Both,
				resize_enabled = true,
				resize_maxHeight = 3000,
				resize_maxWidth = 3000,
				resize_minHeight = 250,
				resize_minWidth = 750,
				scayt_autoStartup = false,
				scayt_contextCommands = ScaytContextCommands.All,
				scayt_contextMenuItemsOrder = (ScaytContextMenuItemsOrder.Suggest | ScaytContextMenuItemsOrder.Moresuggest | ScaytContextMenuItemsOrder.Control),
				scayt_customDictionaryIds = string.Empty,
				scayt_customerid = string.Empty,
				scayt_maxSuggestions = 5,
				scayt_moreSuggestions = ScaytMoreSuggestions.On,
				scayt_sLang = "en_US",
				scayt_srcUrl = string.Empty,
				scayt_uiTabs = "1,1,1",
				scayt_userDictionaryName = string.Empty,
				sharedSpacesBottom = string.Empty,
				sharedSpacesTop = string.Empty,
				shiftEnterMode = EnterMode.P,
				skin = "kama",
				smiley_columns = 8,
				smiley_descriptions = new string[]
				{
					"smiley",
					"sad",
					"wink",
					"laugh",
					"frown",
					"cheeky",
					"blush",
					"surprise",
					"indecision",
					"angry",
					"angel",
					"cool",
					"devil",
					"crying",
					"enlightened",
					"no",
					"yes",
					"heart",
					"broken heart",
					"kiss",
					"mail"
				},
				smiley_images = new string[]
				{
					"regular_smile.gif",
					"sad_smile.gif",
					"wink_smile.gif",
					"teeth_smile.gif",
					"confused_smile.gif",
					"tounge_smile.gif",
					"embaressed_smile.gif",
					"omg_smile.gif",
					"whatchutalkingabout_smile.gif",
					"angry_smile.gif",
					"angel_smile.gif",
					"shades_smile.gif",
					"devil_smile.gif",
					"cry_smile.gif",
					"lightbulb.gif",
					"thumbs_down.gif",
					"thumbs_up.gif",
					"heart.gif",
					"broken_heart.gif",
					"kiss.gif",
					"envelope.gif"
				},
				smiley_path = string.Empty,
				specialChars = new string[]
				{
					"!",
					"\"",
					"#",
					"$",
					"%",
					"&",
					"'",
					"(",
					")",
					"*",
					"+",
					"-",
					".",
					"/",
					"0",
					"1",
					"2",
					"3",
					"4",
					"5",
					"6",
					"7",
					"8",
					"9",
					":",
					";",
					"<",
					"=",
					">",
					"?",
					"@",
					"A",
					"B",
					"C",
					"D",
					"E",
					"F",
					"G",
					"H",
					"I",
					"J",
					"K",
					"L",
					"M",
					"N",
					"O",
					"P",
					"Q",
					"R",
					"S",
					"T",
					"U",
					"V",
					"W",
					"X",
					"Y",
					"Z",
					"[",
					"]",
					"^",
					"_",
					"`",
					"a",
					"b",
					"c",
					"d",
					"e",
					"f",
					"g",
					"h",
					"i",
					"j",
					"k",
					"l",
					"m",
					"n",
					"o",
					"p",
					"q",
					"r",
					"s",
					"t",
					"u",
					"v",
					"w",
					"x",
					"y",
					"z",
					"{",
					"|",
					"}",
					"~",
					"€",
					"‘",
					"’",
					"“",
					"”",
					"–",
					"—",
					"¡",
					"¢",
					"£",
					"¤",
					"¥",
					"¦",
					"§",
					"¨",
					"©",
					"ª",
					"«",
					"¬",
					"®",
					"¯",
					"°",
					"&",
					"²",
					"³",
					"´",
					"µ",
					"¶",
					"·",
					"¸",
					"¹",
					"º",
					"&",
					"¼",
					"½",
					"¾",
					"¿",
					"À",
					"Á",
					"Â",
					"Ã",
					"Ä",
					"Å",
					"Æ",
					"Ç",
					"È",
					"É",
					"Ê",
					"Ë",
					"Ì",
					"Í",
					"Î",
					"Ï",
					"Ð",
					"Ñ",
					"Ò",
					"Ó",
					"Ô",
					"Õ",
					"Ö",
					"×",
					"Ø",
					"Ù",
					"Ú",
					"Û",
					"Ü",
					"Ý",
					"Þ",
					"ß",
					"à",
					"á",
					"â",
					"ã",
					"ä",
					"å",
					"æ",
					"ç",
					"è",
					"é",
					"ê",
					"ë",
					"ì",
					"í",
					"î",
					"ï",
					"ð",
					"ñ",
					"ò",
					"ó",
					"ô",
					"õ",
					"ö",
					"÷",
					"ø",
					"ù",
					"ú",
					"û",
					"ü",
					"ü",
					"ý",
					"þ",
					"ÿ",
					"Œ",
					"œ",
					"Ŵ",
					"Ŷ",
					"ŵ",
					"ŷ",
					"‚",
					"‛",
					"„",
					"…",
					"™",
					"►",
					"•",
					"→",
					"⇒",
					"⇔",
					"♦",
					"≈"
				},
				startupFocus = false,
				startupMode = StartupMode.Wysiwyg,
				startupOutlineBlocks = false,
				startupShowBorders = true,
				stylesheetParser_skipSelectors = "/(^body\\.|^\\.)/i",
				stylesheetParser_validSelectors = "/\\w+\\.\\w+/",
				stylesSet = new string[]
				{
					"default"
				},
				tabIndex = 0,
				tabSpaces = 0,
				templates = "default",
				templates_files = new string[]
				{
					"~/plugins/templates/templates/default.js"
				},
				templates_replaceContent = true,
				theme = "default",
				toolbar = "Full",
				toolbar_Basic = new object[]
				{
					new object[]
					{
						"Bold",
						"Italic",
						"-",
						"NumberedList",
						"BulletedList",
						"-",
						"Link",
						"Unlink",
						"-",
						"About"
					}
				},
				toolbar_Full = new object[]
				{
					new object[]
					{
						"Source",
						"-",
						"Save",
						"NewPage",
						"Preview",
						"-",
						"Templates"
					},
					new object[]
					{
						"Cut",
						"Copy",
						"Paste",
						"PasteText",
						"PasteFromWord",
						"-",
						"Print",
						"SpellChecker",
						"Scayt"
					},
					new object[]
					{
						"Undo",
						"Redo",
						"-",
						"Find",
						"Replace",
						"-",
						"SelectAll",
						"RemoveFormat"
					},
					new object[]
					{
						"Form",
						"Checkbox",
						"Radio",
						"TextField",
						"Textarea",
						"Select",
						"Button",
						"ImageButton",
						"HiddenField"
					},
					"/",
					new object[]
					{
						"Bold",
						"Italic",
						"Underline",
						"Strike",
						"-",
						"Subscript",
						"Superscript"
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
						"BidiLtr",
						"BidiRtl"
					},
					new object[]
					{
						"Link",
						"Unlink",
						"Anchor"
					},
					new object[]
					{
						"Image",
						"Flash",
						"Table",
						"HorizontalRule",
						"Smiley",
						"SpecialChar",
						"PageBreak",
						"Iframe"
					},
					"/",
					new object[]
					{
						"Styles",
						"Format",
						"Font",
						"FontSize"
					},
					new object[]
					{
						"TextColor",
						"BGColor"
					},
					new object[]
					{
						"Maximize",
						"ShowBlocks",
						"-",
						"About"
					}
				},
				toolbarCanCollapse = true,
				toolbarGroupCycling = true,
				toolbarLocation = ToolbarLocation.Top,
				toolbarStartupExpanded = true,
				uiColor = "#D3D3D3",
				useComputedState = true,
				width = string.Empty,
				CKEditorInstanceEventHandler = null,
				CKEditorEventHandler = null,
				ExtraOptions = new Hashtable()
			};
		}

		private CKEditorConfig()
		{
		}

		public CKEditorConfig(string editorPath)
		{
			this.autoGrow_bottomSpace = CKEditorConfig.GlobalConfig.autoGrow_bottomSpace;
			this.autoGrow_maxHeight = CKEditorConfig.GlobalConfig.autoGrow_maxHeight;
			this.autoGrow_minHeight = CKEditorConfig.GlobalConfig.autoGrow_minHeight;
			this.autoGrow_onStartup = CKEditorConfig.GlobalConfig.autoGrow_onStartup;
			this.autoParagraph = CKEditorConfig.GlobalConfig.autoParagraph;
			this.autoUpdateElement = CKEditorConfig.GlobalConfig.autoUpdateElement;
			this.baseFloatZIndex = CKEditorConfig.GlobalConfig.baseFloatZIndex;
			this.baseHref = CKEditorConfig.GlobalConfig.baseHref;
			this.basicEntities = CKEditorConfig.GlobalConfig.basicEntities;
			this.blockedKeystrokes = CKEditorConfig.GlobalConfig.blockedKeystrokes;
			this.bodyClass = CKEditorConfig.GlobalConfig.bodyClass;
			this.bodyId = CKEditorConfig.GlobalConfig.bodyId;
			this.browserContextMenuOnCtrl = CKEditorConfig.GlobalConfig.browserContextMenuOnCtrl;
			this.colorButton_backStyle = CKEditorConfig.GlobalConfig.colorButton_backStyle;
			this.colorButton_colors = CKEditorConfig.GlobalConfig.colorButton_colors;
			this.colorButton_enableMore = CKEditorConfig.GlobalConfig.colorButton_enableMore;
			this.colorButton_foreStyle = CKEditorConfig.GlobalConfig.colorButton_foreStyle;
			this.contentsCss = this.ResolveUrl(CKEditorConfig.GlobalConfig.contentsCss, editorPath);
			this.contentsLangDirection = CKEditorConfig.GlobalConfig.contentsLangDirection;
			this.contentsLanguage = CKEditorConfig.GlobalConfig.contentsLanguage;
			this.coreStyles_bold = CKEditorConfig.GlobalConfig.coreStyles_bold;
			this.coreStyles_italic = CKEditorConfig.GlobalConfig.coreStyles_italic;
			this.coreStyles_strike = CKEditorConfig.GlobalConfig.coreStyles_strike;
			this.coreStyles_subscript = CKEditorConfig.GlobalConfig.coreStyles_subscript;
			this.coreStyles_superscript = CKEditorConfig.GlobalConfig.coreStyles_superscript;
			this.coreStyles_underline = CKEditorConfig.GlobalConfig.coreStyles_underline;
			this.customConfig = CKEditorConfig.GlobalConfig.customConfig;
			this.defaultLanguage = CKEditorConfig.GlobalConfig.defaultLanguage;
			this.devtools_styles = CKEditorConfig.GlobalConfig.devtools_styles;
			this.dialog_backgroundCoverColor = CKEditorConfig.GlobalConfig.dialog_backgroundCoverColor;
			this.dialog_backgroundCoverOpacity = CKEditorConfig.GlobalConfig.dialog_backgroundCoverOpacity;
			this.dialog_buttonsOrder = CKEditorConfig.GlobalConfig.dialog_buttonsOrder;
			this.dialog_magnetDistance = CKEditorConfig.GlobalConfig.dialog_magnetDistance;
			this.dialog_startupFocusTab = CKEditorConfig.GlobalConfig.dialog_startupFocusTab;
			this.disableNativeSpellChecker = CKEditorConfig.GlobalConfig.disableNativeSpellChecker;
			this.disableNativeTableHandles = CKEditorConfig.GlobalConfig.disableNativeTableHandles;
			this.disableObjectResizing = CKEditorConfig.GlobalConfig.disableObjectResizing;
			this.disableReadonlyStyling = CKEditorConfig.GlobalConfig.disableReadonlyStyling;
			this.docType = CKEditorConfig.GlobalConfig.docType;
			this.editingBlock = CKEditorConfig.GlobalConfig.editingBlock;
			this.emailProtection = CKEditorConfig.GlobalConfig.emailProtection;
			this.enableTabKeyTools = CKEditorConfig.GlobalConfig.enableTabKeyTools;
			this.enterMode = CKEditorConfig.GlobalConfig.enterMode;
			this.entities = CKEditorConfig.GlobalConfig.entities;
			this.entities_additional = CKEditorConfig.GlobalConfig.entities_additional;
			this.entities_greek = CKEditorConfig.GlobalConfig.entities_greek;
			this.entities_latin = CKEditorConfig.GlobalConfig.entities_latin;
			this.entities_processNumerical = CKEditorConfig.GlobalConfig.entities_processNumerical;
			this.extraPlugins = CKEditorConfig.GlobalConfig.extraPlugins;
			this.filebrowserBrowseUrl = CKEditorConfig.GlobalConfig.filebrowserBrowseUrl;
			this.filebrowserFlashBrowseUrl = CKEditorConfig.GlobalConfig.filebrowserFlashBrowseUrl;
			this.filebrowserFlashUploadUrl = CKEditorConfig.GlobalConfig.filebrowserFlashUploadUrl;
			this.filebrowserImageBrowseLinkUrl = CKEditorConfig.GlobalConfig.filebrowserImageBrowseLinkUrl;
			this.filebrowserImageBrowseUrl = CKEditorConfig.GlobalConfig.filebrowserImageBrowseUrl;
			this.filebrowserImageUploadUrl = CKEditorConfig.GlobalConfig.filebrowserImageUploadUrl;
			this.filebrowserUploadUrl = CKEditorConfig.GlobalConfig.filebrowserUploadUrl;
			this.filebrowserWindowFeatures = CKEditorConfig.GlobalConfig.filebrowserWindowFeatures;
			this.fillEmptyBlocks = CKEditorConfig.GlobalConfig.fillEmptyBlocks;
			this.find_highlight = CKEditorConfig.GlobalConfig.find_highlight;
			this.font_defaultLabel = CKEditorConfig.GlobalConfig.font_defaultLabel;
			this.font_names = CKEditorConfig.GlobalConfig.font_names;
			this.font_style = CKEditorConfig.GlobalConfig.font_style;
			this.fontSize_defaultLabel = CKEditorConfig.GlobalConfig.fontSize_defaultLabel;
			this.fontSize_sizes = CKEditorConfig.GlobalConfig.fontSize_sizes;
			this.fontSize_style = CKEditorConfig.GlobalConfig.fontSize_style;
			this.forceEnterMode = CKEditorConfig.GlobalConfig.forceEnterMode;
			this.forcePasteAsPlainText = CKEditorConfig.GlobalConfig.forcePasteAsPlainText;
			this.forceSimpleAmpersand = CKEditorConfig.GlobalConfig.forceSimpleAmpersand;
			this.format_address = CKEditorConfig.GlobalConfig.format_address;
			this.format_div = CKEditorConfig.GlobalConfig.format_div;
			this.format_h1 = CKEditorConfig.GlobalConfig.format_h1;
			this.format_h2 = CKEditorConfig.GlobalConfig.format_h2;
			this.format_h3 = CKEditorConfig.GlobalConfig.format_h3;
			this.format_h4 = CKEditorConfig.GlobalConfig.format_h4;
			this.format_h5 = CKEditorConfig.GlobalConfig.format_h5;
			this.format_h6 = CKEditorConfig.GlobalConfig.format_h6;
			this.format_p = CKEditorConfig.GlobalConfig.format_p;
			this.format_pre = CKEditorConfig.GlobalConfig.format_pre;
			this.format_tags = CKEditorConfig.GlobalConfig.format_tags;
			this.fullPage = CKEditorConfig.GlobalConfig.fullPage;
			this.height = CKEditorConfig.GlobalConfig.height;
			this.htmlEncodeOutput = CKEditorConfig.GlobalConfig.htmlEncodeOutput;
			this.ignoreEmptyParagraph = CKEditorConfig.GlobalConfig.ignoreEmptyParagraph;
			this.image_previewText = CKEditorConfig.GlobalConfig.image_previewText;
			this.image_removeLinkByEmptyURL = CKEditorConfig.GlobalConfig.image_removeLinkByEmptyURL;
			this.indentClasses = CKEditorConfig.GlobalConfig.indentClasses;
			this.indentOffset = CKEditorConfig.GlobalConfig.indentOffset;
			this.jqueryOverrideVal = CKEditorConfig.GlobalConfig.jqueryOverrideVal;
			this.indentUnit = CKEditorConfig.GlobalConfig.indentUnit;
			this.justifyClasses = CKEditorConfig.GlobalConfig.justifyClasses;
			this.keystrokes = CKEditorConfig.GlobalConfig.keystrokes;
			this.language = CKEditorConfig.GlobalConfig.language;
			this.menu_groups = CKEditorConfig.GlobalConfig.menu_groups;
			this.newpage_html = CKEditorConfig.GlobalConfig.newpage_html;
			this.pasteFromWordCleanupFile = CKEditorConfig.GlobalConfig.pasteFromWordCleanupFile;
			this.pasteFromWordNumberedHeadingToList = CKEditorConfig.GlobalConfig.pasteFromWordNumberedHeadingToList;
			this.pasteFromWordPromptCleanup = CKEditorConfig.GlobalConfig.pasteFromWordPromptCleanup;
			this.pasteFromWordRemoveFontStyles = CKEditorConfig.GlobalConfig.pasteFromWordRemoveFontStyles;
			this.pasteFromWordRemoveStyles = CKEditorConfig.GlobalConfig.pasteFromWordRemoveStyles;
			this.protectedSource = CKEditorConfig.GlobalConfig.protectedSource;
			this.readOnly = CKEditorConfig.GlobalConfig.readOnly;
			this.removeDialogTabs = CKEditorConfig.GlobalConfig.removeDialogTabs;
			this.removeFormatAttributes = CKEditorConfig.GlobalConfig.removeFormatAttributes;
			this.removeFormatTags = CKEditorConfig.GlobalConfig.removeFormatTags;
			this.removePlugins = CKEditorConfig.GlobalConfig.removePlugins;
			this.resize_dir = CKEditorConfig.GlobalConfig.resize_dir;
			this.resize_enabled = CKEditorConfig.GlobalConfig.resize_enabled;
			this.resize_maxHeight = CKEditorConfig.GlobalConfig.resize_maxHeight;
			this.resize_maxWidth = CKEditorConfig.GlobalConfig.resize_maxWidth;
			this.resize_minHeight = CKEditorConfig.GlobalConfig.resize_minHeight;
			this.resize_minWidth = CKEditorConfig.GlobalConfig.resize_minWidth;
			this.scayt_autoStartup = CKEditorConfig.GlobalConfig.scayt_autoStartup;
			this.scayt_contextCommands = CKEditorConfig.GlobalConfig.scayt_contextCommands;
			this.scayt_contextMenuItemsOrder = CKEditorConfig.GlobalConfig.scayt_contextMenuItemsOrder;
			this.scayt_customDictionaryIds = CKEditorConfig.GlobalConfig.scayt_customDictionaryIds;
			this.scayt_customerid = CKEditorConfig.GlobalConfig.scayt_customerid;
			this.scayt_maxSuggestions = CKEditorConfig.GlobalConfig.scayt_maxSuggestions;
			this.scayt_moreSuggestions = CKEditorConfig.GlobalConfig.scayt_moreSuggestions;
			this.scayt_sLang = CKEditorConfig.GlobalConfig.scayt_sLang;
			this.scayt_srcUrl = CKEditorConfig.GlobalConfig.scayt_srcUrl;
			this.scayt_uiTabs = CKEditorConfig.GlobalConfig.scayt_uiTabs;
			this.scayt_userDictionaryName = CKEditorConfig.GlobalConfig.scayt_userDictionaryName;
			this.sharedSpacesBottom = CKEditorConfig.GlobalConfig.sharedSpacesBottom;
			this.sharedSpacesTop = CKEditorConfig.GlobalConfig.sharedSpacesTop;
			this.shiftEnterMode = CKEditorConfig.GlobalConfig.shiftEnterMode;
			this.skin = CKEditorConfig.GlobalConfig.skin;
			this.smiley_columns = CKEditorConfig.GlobalConfig.smiley_columns;
			this.smiley_descriptions = CKEditorConfig.GlobalConfig.smiley_descriptions;
			this.smiley_images = CKEditorConfig.GlobalConfig.smiley_images;
			this.smiley_path = CKEditorConfig.GlobalConfig.smiley_path;
			this.specialChars = CKEditorConfig.GlobalConfig.specialChars;
			this.startupFocus = CKEditorConfig.GlobalConfig.startupFocus;
			this.startupMode = CKEditorConfig.GlobalConfig.startupMode;
			this.startupOutlineBlocks = CKEditorConfig.GlobalConfig.startupOutlineBlocks;
			this.startupShowBorders = CKEditorConfig.GlobalConfig.startupShowBorders;
			this.stylesheetParser_skipSelectors = CKEditorConfig.GlobalConfig.stylesheetParser_skipSelectors;
			this.stylesheetParser_validSelectors = CKEditorConfig.GlobalConfig.stylesheetParser_validSelectors;
			this.stylesSet = CKEditorConfig.GlobalConfig.stylesSet;
			this.tabIndex = CKEditorConfig.GlobalConfig.tabIndex;
			this.tabSpaces = CKEditorConfig.GlobalConfig.tabSpaces;
			this.templates = CKEditorConfig.GlobalConfig.templates;
			this.templates_files = this.ResolveUrl(CKEditorConfig.GlobalConfig.templates_files, editorPath);
			this.templates_replaceContent = CKEditorConfig.GlobalConfig.templates_replaceContent;
			this.theme = CKEditorConfig.GlobalConfig.theme;
			this.toolbar = CKEditorConfig.GlobalConfig.toolbar;
			this.toolbar_Basic = CKEditorConfig.GlobalConfig.toolbar_Basic;
			this.toolbar_Full = CKEditorConfig.GlobalConfig.toolbar_Full;
			this.toolbarCanCollapse = CKEditorConfig.GlobalConfig.toolbarCanCollapse;
			this.toolbarGroupCycling = CKEditorConfig.GlobalConfig.toolbarGroupCycling;
			this.toolbarLocation = CKEditorConfig.GlobalConfig.toolbarLocation;
			this.uiColor = CKEditorConfig.GlobalConfig.uiColor;
			this.toolbarStartupExpanded = CKEditorConfig.GlobalConfig.toolbarStartupExpanded;
			this.useComputedState = CKEditorConfig.GlobalConfig.useComputedState;
			this.width = CKEditorConfig.GlobalConfig.width;
			this.ExtraOptions = new Hashtable(CKEditorConfig.GlobalConfig.ExtraOptions);
			this.CKEditorEventHandler = null;
			bool flag = this.CKEditorInstanceEventHandler != null;
			if (flag)
			{
				this.CKEditorInstanceEventHandler = new List<object>(CKEditorConfig.GlobalConfig.CKEditorInstanceEventHandler);
			}
		}

		private string[] ResolveUrl(string[] value, string resolvedStr)
		{
			for (int i = 0; i < value.Length; i++)
			{
				bool flag = value[i].StartsWith("~") && !value[i].StartsWith(resolvedStr);
				if (flag)
				{
					value[i] = resolvedStr + value[i].Replace("~", "");
				}
			}
			return value;
		}

		private void ResolveParameters(string[] parIn, List<string> retVal, bool addAp)
		{
			for (int i = 0; i < parIn.Length; i++)
			{
				string item = parIn[i];
				bool flag = item.StartsWith("[") || item.EndsWith("]");
				if (flag)
				{
					this.ResolveParameters(item.Trim().Replace("[", string.Empty).Replace("]", string.Empty).Split(new string[]
					{
						","
					}, StringSplitOptions.RemoveEmptyEntries), retVal, addAp);
				}
				else
				{
					bool flag2 = addAp && !item.StartsWith("'") && !item.EndsWith("'");
					if (flag2)
					{
						retVal.Add("'" + item.Trim().Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty) + "'");
					}
					else
					{
						retVal.Add(item.Trim().Replace("\t", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty));
					}
				}
			}
		}

		private string ResolveParameters(string parIn, bool addAp)
		{
			bool flag = parIn.StartsWith("[") && parIn.EndsWith("]");
			string result;
			if (flag)
			{
				result = parIn;
			}
			else
			{
				bool flag2 = addAp && !parIn.StartsWith("'") && !parIn.EndsWith("'");
				if (flag2)
				{
					result = "'" + parIn + "'";
				}
				else
				{
					result = parIn;
				}
			}
			return result;
		}
	}
}

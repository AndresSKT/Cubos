//
//  LanguageManager.cs
//
// Copyright (c) 2013 Niklas Borglund. All rights reserved.
// @NiklasBorglund

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Globalization;

/// <summary>
/// Change language event handler.
/// </summary>
public delegate void ChangeLanguageEventHandler(LanguageManager thisLanguage);

public class LanguageManager : MonoBehaviour
{
	#region internalCultureDic
	static Dictionary<string,string> internalCultureDictionary = new Dictionary<string, string>(){
		{"Arabic","ar"},
		{"Bulgarian","bg"},
		{"Catalan","ca"},
		{"Chinese","zh-CHS"},
		{"Czech","cs"},
		{"Danish","da"},
		{"German","de"},
		{"Greek","el"},
		{"English","en"},
		{"Spanish","es"},
		{"Finnish","fi"},
		{"French","fr"},
		{"Hebrew","he"},
		{"Hungarian","hu"},
		{"Icelandic","is"},
		{"Italian","it"},
		{"Japanese","ja"},
		{"Korean","ko"},
		{"Dutch","nl"},
		{"Norwegian","no"},
		{"Polish","pl"},
		{"Portuguese","pt"},
		{"Romanian","ro"},
		{"Russian","ru"},
		{"Croatian","hr"},
		{"Slovak","sk"},
		{"Albanian","sq"},
		{"Swedish","sv"},
		{"Thai","th"},
		{"Turkish","tr"},
		{"Indonesian","id"},
		{"Ukrainian","uk"},
		{"Belarusian","be"},
		{"Slovenian","sl"},
		{"Estonian","et"},
		{"Latvian","lv"},
		{"Lithuanian","lt"},
		{"Persian","fa"},
		{"Vietnamese","vi"},
		{"Armenian","hy"},
		{"Basque","eu"},
		{"Macedonian","mk"},
		{"Afrikaans","af"},
		{"Georgian","ka"},
		{"Faroese","fo"},
		{"Hindi","hi"},
		{"Swahili","sw"},
		{"Gujarati","gu"},
		{"Tamil","ta"},
		{"Telugu","te"},
		{"Kannada","kn"},
		{"Marathi","mr"},
		{"Gallegan","gl"},
		{"Konkani","kok"},
		{"Arabic (Saudi Arabia)","ar-SA"},
		{"Bulgarian (Bulgaria)","bg-BG"},
		{"Catalan (Spain)","ca-ES"},
		{"Chinese (Taiwan)","zh-TW"},
		{"Czech (Czech Republic)","cs-CZ"},
		{"Danish (Denmark)","da-DK"},
		{"German (Germany)","de-DE"},
		{"Greek (Greece)","el-GR"},
		{"English (United States)","en-US"},
		{"Finnish (Finland)","fi-FI"},
		{"French (France)","fr-FR"},
		{"Hebrew (Israel)","he-IL"},
		{"Hungarian (Hungary)","hu-HU"},
		{"Icelandic (Iceland)","is-IS"},
		{"Italian (Italy)","it-IT"},
		{"Japanese (Japan)","ja-JP"},
		{"Korean (South Korea)","ko-KR"},
		{"Dutch (Netherlands)","nl-NL"},
		{"Norwegian Bokmål (Norway)","nb-NO"},
		{"Polish (Poland)","pl-PL"},
		{"Portuguese (Brazil)","pt-BR"},
		{"Romanian (Romania)","ro-RO"},
		{"Russian (Russia)","ru-RU"},
		{"Croatian (Croatia)","hr-HR"},
		{"Slovak (Slovakia)","sk-SK"},
		{"Albanian (Albania)","sq-AL"},
		{"Swedish (Sweden)","sv-SE"},
		{"Thai (Thailand)","th-TH"},
		{"Turkish (Turkey)","tr-TR"},
		{"Indonesian (Indonesia)","id-ID"},
		{"Ukrainian (Ukraine)","uk-UA"},
		{"Belarusian (Belarus)","be-BY"},
		{"Slovenian (Slovenia)","sl-SI"},
		{"Estonian (Estonia)","et-EE"},
		{"Latvian (Latvia)","lv-LV"},
		{"Lithuanian (Lithuania)","lt-LT"},
		{"Persian (Iran)","fa-IR"},
		{"Vietnamese (Vietnam)","vi-VN"},
		{"Armenian (Armenia)","hy-AM"},
		{"Basque (Spain)","eu-ES"},
		{"Macedonian (Macedonia)","mk-MK"},
		{"Afrikaans (South Africa)","af-ZA"},
		{"Georgian (Georgia)","ka-GE"},
		{"Faroese (Faroe Islands)","fo-FO"},
		{"Hindi (India)","hi-IN"},
		{"Swahili (Kenya)","sw-KE"},
		{"Gujarati (India)","gu-IN"},
		{"Tamil (India)","ta-IN"},
		{"Telugu (India)","te-IN"},
		{"Kannada (India)","kn-IN"},
		{"Marathi (India)","mr-IN"},
		{"Gallegan (Spain)","gl-ES"},
		{"Konkani (India)","kok-IN"},
		{"Arabic (Iraq)","ar-IQ"},
		{"Chinese (China)","zh-CN"},
		{"German (Switzerland)","de-CH"},
		{"English (United Kingdom)","en-GB"},
		{"Spanish (Mexico)","es-MX"},
		{"French (Belgium)","fr-BE"},
		{"Italian (Switzerland)","it-CH"},
		{"Dutch (Belgium)","nl-BE"},
		{"Norwegian Nynorsk (Norway)","nn-NO"},
		{"Portuguese (Portugal)","pt-PT"},
		{"Swedish (Finland)","sv-FI"},
		{"Arabic (Egypt)","ar-EG"},
		{"Chinese (Hong Kong S.A.R., China)","zh-HK"},
		{"German (Austria)","de-AT"},
		{"English (Australia)","en-AU"},
		{"Spanish (Spain)","es-ES"},
		{"French (Canada)","fr-CA"},
		{"Arabic (Libya)","ar-LY"},
		{"Chinese (Singapore)","zh-SG"},
		{"German (Luxembourg)","de-LU"},
		{"English (Canada)","en-CA"},
		{"Spanish (Guatemala)","es-GT"},
		{"French (Switzerland)","fr-CH"},
		{"Arabic (Algeria)","ar-DZ"},
		{"Chinese (Macao S.A.R. China)","zh-MO"},
		{"English (New Zealand)","en-NZ"},
		{"Spanish (Costa Rica)","es-CR"},
		{"French (Luxembourg)","fr-LU"},
		{"Arabic (Morocco)","ar-MA"},
		{"English (Ireland)","en-IE"},
		{"Spanish (Panama)","es-PA"},
		{"Arabic (Tunisia)","ar-TN"},
		{"English (South Africa)","en-ZA"},
		{"Spanish (Dominican Republic)","es-DO"},
		{"Arabic (Oman)","ar-OM"},
		{"Spanish (Venezuela)","es-VE"},
		{"Arabic (Yemen)","ar-YE"},
		{"Spanish (Colombia)","es-CO"},
		{"Arabic (Syria)","ar-SY"},
		{"Spanish (Peru)","es-PE"},
		{"Arabic (Jordan)","ar-JO"},
		{"English (Trinidad and Tobago)","en-TT"},
		{"Spanish (Argentina)","es-AR"},
		{"Arabic (Lebanon)","ar-LB"},
		{"English (Zimbabwe)","en-ZW"},
		{"Spanish (Ecuador)","es-EC"},
		{"Arabic (Kuwait)","ar-KW"},
		{"English (Philippines)","en-PH"},
		{"Spanish (Chile)","es-CL"},
		{"Arabic (United Arab Emirates)","ar-AE"},
		{"Spanish (Uruguay)","es-UY"},
		{"Arabic (Bahrain)","ar-BH"},
		{"Spanish (Paraguay)","es-PY"},
		{"Arabic (Qatar)","ar-QA"},
		{"Spanish (Bolivia)","es-BO"},
		{"Spanish (El Salvador)","es-SV"},
		{"Spanish (Honduras)","es-HN"},
		{"Spanish (Nicaragua)","es-NI"},
		{"Spanish (Puerto Rico)","es-PR"}
	};
	#endregion

    #region Singleton
    private static LanguageManager instance = null;
    public static LanguageManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<LanguageManager>();
                go.name = "LanguageManager";
            }

            return instance;
        }
    }
	public static bool HasInstance
	{
		get
		{
			return (instance != null);
		}
	}
    #endregion
	
	/// <summary>
	/// Occurs when a new language is loaded and initialized
	/// create a delegate method(ChangeLanguage(LanguageManager thisLanguage, CultureInfo newLanguage)) and subscribe
	/// </summary>
	public event ChangeLanguageEventHandler OnChangeLanguage;
	
	/// <summary>
	/// The language that the system will try and load if LoadResources is called.
	/// </summary>
	public string language = "en";
	/// <summary>
	/// The default language.
	/// </summary>
    public string defaultLanguage = "en";
	
	/// <summary>
	/// The path to use in the Resources.Load Method.
	/// </summary>
    private string resourceFile = "Localization/Generated Assets/Language";

	/// <summary>  The header to add to the xmlFile after the unwanted resx-stuff is removed(To read the file with XMLReader)  </summary>
    private string xmlHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<root>";
	/// <summary> Dictionary with the raw text elements   </summary>
	private SortedDictionary<string, string> textDataBase = new SortedDictionary<string,string>();
	/// <summary> The parsed localizedObject list  </summary>
	private SortedDictionary<string, LocalizedObject> localizedObjectDataBase = new SortedDictionary<string, LocalizedObject>();
	
	/// <summary>
	/// a bool which indicates if a language is loaded
	/// </summary>
    private bool initialized = false;
	
	//A list of all the available languages
	private List<string> availableLanguages = new List<string>();
	/// <summary>
	/// Gets a list of the available languages.
	/// </summary>
	/// <value>
	/// The available languages.
	/// </value>
	public List<string> AvailableLanguages
	{
		get
		{
			return availableLanguages;	
		}
	}
	
#if !UNITY_WP8
	//Same as availableLanguages, but contains more info in the form of System.Globalization.CultureInfo
	private List<CultureInfo> availableLanguagesCultureInfo = new List<CultureInfo>();
	/// <summary>
	/// Gets the available languages culture info.(Same as availableLanguages, but contains more info in the form of System.Globalization.CultureInfo)
	/// </summary>
	/// <value>
	/// The available languages culture info.
	/// </value>
	public List<CultureInfo> AvailableLanguagesCultureInfo
	{
		get
		{
			return availableLanguagesCultureInfo;	
		}
	}
#endif
	
	/// <summary>
	/// a bool which indicates if a language is loaded
	/// </summary>
	/// <value>
	/// <c>true</c> if a language is loaded; otherwise, <c>false</c>.
	/// </value>
    public bool IsInitialized
    {
        get { return initialized; }
    }
	
	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		
		if(PlayerPrefs.HasKey("cws_defaultLanguage"))
		{
			defaultLanguage = PlayerPrefs.GetString("cws_defaultLanguage");	
		}
		
		GetAvailableLanguages();

		Debug.Log ("LanguageManager.cs: Waking up");
		
		//Load the default language(if it exists)
		foreach(string availableLanguage in availableLanguages)
		{
			if(availableLanguage == defaultLanguage)
			{
				ChangeLanguage(availableLanguage);
				break;
			}
		}
		
		//otherwise - load the first language in the list
		if(!initialized && availableLanguages.Count > 0)
		{
			ChangeLanguage(availableLanguages[0]);
		}
		else if(!initialized)
		{
			Debug.LogError("LanguageManager.cs: No language is available! Use Window->Smart Localization tool to create a language");	
		}
	}
	void OnDestroy()
	{
		//Clear the event handler
		OnChangeLanguage = null;
	}
	
	/// <summary>
	/// Loads the language file, language is specified with the language variable
	/// </summary>
    private void LoadResources()
    {
        //Reset values
        initialized = false;
        textDataBase.Clear();
		localizedObjectDataBase.Clear();
		
		//Load the root file if language is null
		TextAsset languageDocument;
		if(language == null)
		{
			languageDocument = Resources.Load(resourceFile) as TextAsset;
		}
		else
		{
			languageDocument = Resources.Load(resourceFile + "." + language) as TextAsset;
		}

        if (!languageDocument && defaultLanguage != language)
        {
            //If the language does not exist, revert back to the default language and reload
            Debug.LogError("ERROR: Language file:" + language + " could not be found! - reverting to default language:" + defaultLanguage);
            ChangeLanguage(defaultLanguage);
            return;
        }
		else if(!languageDocument)
		{
			Debug.LogError("ERROR: Language file:" + language + " could not be found!");
			return;
		}

        //Remove beginning of the file that we don't need 
        //The part of the .resx file that  
        int length = "</xsd:schema>".Length;
        string resxDocument = languageDocument.text;
        int index = resxDocument.IndexOf("</xsd:schema>");
        index += length;
        string xmlDocument = resxDocument.Substring(index);

        //add the header to the document
        xmlDocument = xmlHeader + xmlDocument;

		#if !UNITY_METRO 
        //Create the xml file with the new reduced resx document
        XmlReader reader = XmlReader.Create(new StringReader(xmlDocument));

        //read through the document and save the data
        ReadElements(reader);
        
        //done
        reader.Close();
		#else
		using (XmlReader reader = XmlReader.Create(new StringReader(xmlDocument))){
			ReadElements(reader);

		}
		#endif
        initialized = true;
    }
	/// <summary>
	/// Reads the elements from the loaded xmldocument in LoadResources
	/// </summary>
	/// <param name='reader'>
	/// Reader.
	/// </param>
    private void ReadElements(XmlReader reader)
    {
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    //If this is a chunk of data, then parse it
				if (reader.Name == "data")
                    {
					ReadData(reader);
						
				}
                    break;
            }
        }
    }
	/// <summary>
	/// Reads a specific data tag from the xml document.
	/// </summary>
	/// <param name='reader'>
	/// Reader.
	/// </param>
    private void ReadData(XmlReader reader)
    {
        //If these values are not being set,
        //something is wrong.
		string key = "ERROR";

        string value = "ERROR";

        if (reader.HasAttributes)
        {
            while (reader.MoveToNextAttribute())
            {
                if (reader.Name == "name")
                {
                    key = reader.Value;
                }
            }
		}

        //Move back to the element
        reader.MoveToElement();
		//Read the child nodes
        if (reader.ReadToDescendant("value"))
        {
            do
            {
				//value = reader.ReadString();
				value = reader.ReadElementContentAsString();
				if (reader.Name.Equals("data")){
					break;
				}
			}
            while (reader.ReadToNextSibling("value"));
			}

        //Add the raw values to the dictionary
        textDataBase.Add(key, value);
		
		//Add the localized parsed values to the localizedObjectDataBase
		LocalizedObject newLocalizedObject = new LocalizedObject();
		newLocalizedObject.ObjectType = LocalizedObject.GetLocalizedObjectType(key);
		newLocalizedObject.TextValue = value;
		localizedObjectDataBase.Add(LocalizedObject.GetCleanKey(key,newLocalizedObject.ObjectType), newLocalizedObject); 
    }
	/// <summary>
	/// Gets all the available languages.
	/// </summary>
	private void GetAvailableLanguages()
	{
		//Clear the available languages list
		availableLanguages.Clear();
#if !UNITY_WP8
		availableLanguagesCultureInfo.Clear();
#endif
		
		Object[] languageFiles = Resources.LoadAll("Localization/Generated Assets", typeof(TextAsset));
		string languageStart = "Language.";
		foreach(Object languageFile in languageFiles)
		{
			if(languageFile.name != "Language") //Skip the root file
			{
				if(languageFile.name.StartsWith(languageStart))
				{
					string thisLanguageName = languageFile.name.Substring(languageStart.Length);
					availableLanguages.Add(thisLanguageName);
#if !UNITY_WP8
					availableLanguagesCultureInfo.Add(new CultureInfo(thisLanguageName));
#endif
				}
			}
		}
		
	}
	
	/// <summary>
	/// Returns a text value in the current language for the key. Returns null if nothing is found.
	/// </summary>
	/// <returns>
	/// The value in the specified language, returns null if nothing is found
	/// </returns>
	/// <param name='key'>
	/// The Language Key.
	/// </param>
    public string GetTextValue(string key)
    {
		LocalizedObject thisObject = GetLocalizedObject(key);
      
		if(thisObject != null)
		{
			return thisObject.TextValue;	
		}

		Debug.LogError("LanguageManager.cs: Invalid Key:" + key + "for language: " + language);
        return null;
    }
	/// <summary>
	/// Gets the audio clip for the current language, returns null if nothing is found
	/// </summary>
	/// <returns>
	/// The audio clip. Null if nothing is found
	/// </returns>
	/// <param name='key'>
	/// Key.
	/// </param>
	public AudioClip GetAudioClip(string key)
	{
		LocalizedObject thisObject = GetLocalizedObject(key);
      
		if(thisObject != null)
		{
			return Resources.Load("Localization/" + language + "/Audio Files/" + key) as AudioClip;
		}

        return null;
	}
	/// <summary>
	/// Gets the prefab game object for the current language, returns null if nothing is found
	/// </summary>
	/// <returns>
	/// The loaded prefab. Null if nothing is found
	/// </returns>
	/// <param name='key'>
	/// Key.
	/// </param>
	public GameObject GetPrefab(string key)
	{
		LocalizedObject thisObject = GetLocalizedObject(key);
      
		if(thisObject != null)
		{
			return Resources.Load("Localization/" + language + "/Prefabs/" + key) as GameObject;
		}

        return null;
	}	
	/// <summary>
	/// Gets the texture for the current language, returns null if nothing is found
	/// </summary>
	/// <returns>
	/// The loaded prefab. Null if nothing is found
	/// </returns>
	/// <param name='key'>
	/// Key.
	/// </param>
	public Texture GetTexture(string key)
	{
		LocalizedObject thisObject = GetLocalizedObject(key);
      
		if(thisObject != null)
		{
			return Resources.Load("Localization/" + language + "/Textures/" + key) as Texture;
		}

        return null;
	}
	
	/// <summary>
	/// Gets the localized object from the localizedObjectDataBase
	/// </summary>
	/// <returns>
	/// The localized object. Returns null if nothing is found
	/// </returns>
	/// <param name='key'>
	/// Key.
	/// </param>
	private LocalizedObject GetLocalizedObject(string key)
	{
		LocalizedObject thisObject;
		localizedObjectDataBase.TryGetValue(key, out thisObject);
      

        return thisObject;
	}
	
	/// <summary>
	/// Changes the language and tries to load it.
	/// </summary>
	/// <param name='language'>
	/// Language.
	/// </param>
    public void ChangeLanguage(string language)
    {
        this.language = language;
        LoadResources();
		
		if(IsInitialized && OnChangeLanguage != null)
		{
			OnChangeLanguage(this);	
		}
    }
	/// <summary>
	/// Returns the entire RAW language database from the loaded language in a Dictionary
	/// it contains type keys and everything
	/// </summary>
	/// <returns>
	/// The text data base.
	/// </returns>
	public Dictionary<string, string> GetTextDataBase()
	{
		//Convert the sorted dictionary to a new, regular one
		return new Dictionary<string,string>(textDataBase);	
	}
	/// <summary>
	/// Gets the localized, clean and parsed object data base. 
	/// </summary>
	/// <returns>
	/// The localized object data base.
	/// </returns>
	public Dictionary<string, LocalizedObject> GetLocalizedObjectDataBase()
	{
		//Convert the sorted dictionary to a new, regular one
		return new Dictionary<string,LocalizedObject>(localizedObjectDataBase);	
	}
	/// <summary>
	/// Clear this instance and Destroys it
	/// </summary>
	public void Clear ()
	{
		instance = null;
		DestroyImmediate(this.gameObject);
	}
	/// <summary>
	/// Sets the default language. This language will be loaded in Awake() if it exists
	/// By default this is set to = "en"
	/// </summary>
	/// <param name='languageName'>
	/// Language name.
	/// </param>
	public void SetDefaultLanguage(string languageName)
	{
		PlayerPrefs.SetString("cws_defaultLanguage", languageName);
	}
	/// <summary>
	/// Sets the default language. This language will be loaded in Awake() if it exists
	/// By default this is set to = "en"
	/// </summary>
	/// <param name='languageName'>
	/// Language name.
	/// </param>
	public void SetDefaultLanguage(CultureInfo languageInfo)
	{
		SetDefaultLanguage(languageInfo.Name);
	}
	/// <summary>
	/// Checks if the language is supported by this application
	/// languageName = strings like "en", "es", "sv"
	/// </summary>
	public bool IsLanguageSupported(string languageName)
	{
		return availableLanguages.Contains(languageName);
	}
	/// <summary>
	/// Checks if the language is supported by this application
	/// </summary>
	public bool IsLanguageSupported(CultureInfo cultureInfo)
	{
		return IsLanguageSupported(cultureInfo.Name);
	}
	/// <summary>
	/// Gets the system language for this application using Application.systemLanguage
	/// If its SystemLanguage.Unknown, a string with the value "Unknown" will be returned
	/// </summary>
	/// <returns>
	/// The system language.
	/// </returns>
	public string GetSystemLanguage()
	{
		if(Application.systemLanguage == SystemLanguage.Unknown)
		{
			Debug.LogWarning("LanguageManager.cs: The system language of this application is Unknown");
			return "Unknown";
		}
		
		string systemLanguage = Application.systemLanguage.ToString();
		#if (!UNITY_WINRT && !UNITY_METRO && !UNITY_WP8)
		CultureInfo[] cultureInfos = CultureInfo.GetCultures(CultureTypes.AllCultures);
		foreach(CultureInfo info in cultureInfos)
		{
			if(info.EnglishName == systemLanguage)
			{
				return info.Name;	
			}
		}
		#endif
		if (internalCultureDictionary.ContainsKey (systemLanguage)) {
			return internalCultureDictionary[systemLanguage];
		}
		
		Debug.LogError("LanguageManager.cs: A system language of this application is could not be found!");
		return "System Language not found!";
	}
	/// <summary>
	/// Gets the culture info of the specified string
	/// languageName = strings like "en", "es", "sv"
	/// </summary>
	public CultureInfo GetCultureInfo(string languageName)
	{
		return new CultureInfo(languageName);
	}
}

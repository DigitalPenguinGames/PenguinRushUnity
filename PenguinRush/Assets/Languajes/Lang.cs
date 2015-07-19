/*
The Lang Class adds easy to use multiple language support to any Unity project by parsing an XML file
containing numerous strings translated into any languages of your choice.  Refer to UMLS_Help.html and lang.xml
for more information.
 
Created by Adam T. Ryder
C# version by O. Glorieux
Refactored by Pinkii
 
*/

using System;
using System.Collections;
using System.IO;
using System.Xml;

using UnityEngine;

public class Lang
{
	private Hashtable Strings;
	private Hashtable Defaults;
	private bool differentLangs;
	private string defaultLang = "English";
	
	/*
    Initialize Lang class
    path = path to XML resource example:  Path.Combine(Application.dataPath, "lang.xml")
    language = language to use example:  "English"
 
    NOTE:
    If XML resource is on-line rather than local do not supply the path to the path variable as stated above
    instead use the WWW class to download the resource and then supply the resource.text to this initializer
     
    Lang lang = new Lang(wwwXML.text, currentLang)
    */
	public Lang ( string path, string language) {
		if (language == defaultLang) {
			setLanguage(path, "Default");
			differentLangs = false;
		}
		else {
			setLanguage(path, language);
			setLanguageD(path);
			differentLangs = true;
		}
	}
	
	/*
    Use the setLanguage function to swap languages after the Lang class has been initialized.
    This function is called automatically when the Lang class is initialized.
    path = path to XML resource example:  Path.Combine(Application.dataPath, "lang.xml")
    language = language to use example:  "English"
 
    NOTE:
    If the XML resource is stored on the web rather than on the local system use the
    setLanguageWeb function
    */
	public void setLanguage ( string path, string language) {
		var xml = new XmlDocument();
		xml.Load(path);
		
		Strings = new Hashtable();
		var element = xml.DocumentElement[language];
		if (element != null) {
			var elemEnum = element.GetEnumerator();
			while (elemEnum.MoveNext()) {
				var xmlItem = (XmlElement)elemEnum.Current;
				Strings.Add(xmlItem.GetAttribute("name"), xmlItem.InnerText);
			}
		} else {
			Debug.LogError("The specified language does not exist: " + language);
		}
	}
	public void setLanguageD ( string path) {
		var xml = new XmlDocument();
		xml.Load(path);
		
		Defaults = new Hashtable();
		var element = xml.DocumentElement["Default"];
		if (element != null) {
			var elemEnum = element.GetEnumerator();
			while (elemEnum.MoveNext()) {
				var xmlItem = (XmlElement)elemEnum.Current;
				Defaults.Add(xmlItem.GetAttribute("name"), xmlItem.InnerText);
			}
		} else {
			Debug.LogError("The specified language does not exist: " + "Default");
		}
	}

	
	/*
    Access strings in the currently selected language by supplying this getString function with
    the name identifier for the string used in the XML resource.
 
    Example:
    XML file:
    <languages>
        <English>
            <string name="app_name">Unity Multiple Language Support</string>
            <string name="description">This script provides convenient multiple language support.</string>
        </English>
        <French>
            <string name="app_name">Unité Langue Soutien Multiple</string>
            <string name="description">Ce script fournit un soutien multilingue pratique.</string>
        </French>
    </languages>
 
    C#:
    String s = langClass.getString("app_name");
    */
	public string getString (string name) {
		if (!Strings.ContainsKey(name)) {

			if (Defaults.ContainsKey(name) && differentLangs) {
				return (string)Defaults[name];
			}
			Debug.LogError("The specified string does not exist: " + name);
			return "";
		}
		return (string)Strings[name];
	}
	
}
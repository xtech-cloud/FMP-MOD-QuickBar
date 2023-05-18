
using System.Xml.Serialization;

namespace XTC.FMP.MOD.QuickBar.LIB.Unity
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class MyConfig : MyConfigBase
    {
        public class Layout
        {
            [XmlAttribute("orient")]
            public string orient { get; set; } = "horizontal";
            [XmlAttribute("marginLeft")]
            public int marginLeft { get; set; } = 0;
            [XmlAttribute("marginRight")]
            public int marginRight { get; set; } = 0;
            [XmlAttribute("marginTop")]
            public int marginTop { get; set; } = 0;
            [XmlAttribute("marginBottom")]
            public int marginBottom { get; set; } = 0;
            [XmlAttribute("spacing")]
            public int spacing { get; set; } = 0;
            [XmlAttribute("entryWidth")]
            public int entryWidth{ get; set; } = 64;
            [XmlAttribute("entryHeight")]
            public int entryHeight{ get; set; } = 64;
        }

        public class Border
        {
            [XmlAttribute("left")]
            public int left { get; set; } = 0;
            [XmlAttribute("right")]
            public int right { get; set; } = 0;
            [XmlAttribute("top")]
            public int top { get; set; } = 0;
            [XmlAttribute("bottom")]
            public int bottom { get; set; } = 0;
        }

        public class Bar : UiElement
        {
            [XmlElement("Border")]
            public Border border { get; set; } = new Border();
            [XmlElement("Layout")]
            public Layout layout { get; set; } = new Layout();
        }

        public class Entry
        {
            [XmlAttribute("icon")]
            public string icon { get; set; } = "";

            [XmlArray("SubjectS"), XmlArrayItem("Subject")]
            public Subject[] subjectS { get; set; } = new Subject[0];
        }

        public class Style
        {
            [XmlAttribute("name")]
            public string name { get; set; } = "";
            [XmlElement("Bar")]
            public Bar bar { get; set; } = new Bar();
            [XmlArray("EntryS"), XmlArrayItem("Entry")]
            public Entry[] entryS { get; set; } = new Entry[0];
        }


        [XmlArray("Styles"), XmlArrayItem("Style")]
        public Style[] styles { get; set; } = new Style[0];
    }
}




using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.QuickBar.LIB.Proto;
using XTC.FMP.MOD.QuickBar.LIB.MVCS;

namespace XTC.FMP.MOD.QuickBar.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {
        private class UiReference
        {
            public RectTransform bar;
            public RectTransform templateEntry;
        }

        private UiReference uiReference_ = new UiReference();

        public MyInstance(string _uid, string _style, MyConfig _config, MyCatalog _catalog, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _catalog, _logger, _settings, _entry, _mono, _rootAttachments)
        {
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        /// <remarks>
        /// 可用于加载主题目录的数据
        /// </remarks>
        public void HandleCreated()
        {
            uiReference_.bar = rootUI.transform.Find("Bar").GetComponent<RectTransform>();
            uiReference_.templateEntry = rootUI.transform.Find("Bar/templateEntry").GetComponent<RectTransform>();
            uiReference_.templateEntry.gameObject.SetActive(false);

            loadTextureFromTheme(style_.bar.image, (_texture) =>
            {
                var border = new Vector4(style_.bar.border.left, style_.bar.border.bottom, style_.bar.border.right, style_.bar.border.top);
                var sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 100, 1, SpriteMeshType.Tight, border);
                uiReference_.bar.GetComponent<Image>().sprite = sprite;
            }, () => { });
            alignByAncor(uiReference_.bar.transform, style_.bar.anchor);

            // 应用布局样式
            var layoutGroup = uiReference_.bar.GetComponent<GridLayoutGroup>();
            if ("horizontal" == style_.bar.layout.orient)
                layoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            else if ("vertical" == style_.bar.layout.orient)
                layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layoutGroup.padding = new RectOffset(style_.bar.layout.marginLeft, style_.bar.layout.marginRight, style_.bar.layout.marginTop, style_.bar.layout.marginBottom);
            layoutGroup.spacing = new Vector2(style_.bar.layout.spacing, style_.bar.layout.spacing);
            layoutGroup.cellSize = new Vector2(style_.bar.layout.entryWidth, style_.bar.layout.entryHeight);

            // 创建入口
            foreach (var entry in style_.entryS)
            {
                var clone = GameObject.Instantiate(uiReference_.templateEntry.gameObject, uiReference_.templateEntry.parent);
                clone.gameObject.SetActive(true);
                clone.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Dictionary<string, object> variableS = new Dictionary<string, object>();
                    publishSubjects(entry.subjectS, variableS);
                });
                loadTextureFromTheme(entry.icon, (_texture) =>
                {
                    clone.GetComponent<RawImage>().texture = _texture;
                }, () => { });
            }
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        /// <remarks>
        /// 可用于加载内容目录的数据
        /// </remarks>
        public void HandleOpened(string _source, string _uri)
        {
            rootUI.gameObject.SetActive(true);
            rootWorld.gameObject.SetActive(true);
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
            rootWorld.gameObject.SetActive(false);
        }
    }
}

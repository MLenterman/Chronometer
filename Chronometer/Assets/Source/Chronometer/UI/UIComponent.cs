using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chronometer
{
    public class UIComponent : MonoBehaviour
    {
        protected string componentName;

        protected bool isEnabled = true;
        protected bool isVisible = true;
        protected bool isInteractible = true;

        protected object userData;

        protected Vector2 size = Vector2.zero;
        protected Vector2 sizeMin = Vector2.zero;
        protected Vector2 sizeMax = Vector2.zero;

        protected Vector3 scale = Vector3.one;
        protected int zIndex = -1;

        protected Color32 enabledColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        protected Color32 disabledColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

        protected UIComponent tooltipComponent;
        protected string tooltipText = "";
        protected bool isTooltipShowing;

        protected bool isMouseHovering;
        protected float mouseHoverStartTime;

        protected UIComponent parentComponent;
        protected List<UIComponent> childComponents = new List<UIComponent>();

        public bool IsEnabled
        {
            get
            {
                if (!enabled)
                    return false;

                if (gameObject != null && !gameObject.activeSelf)
                    return false;

                if (parentComponent == null)
                    return isEnabled;

                if (isEnabled)
                    return parentComponent.IsEnabled;

                return false;
            }

            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;
                    OnEnabledChanged();
                }
            }
        }

        public bool IsVisible
        {
            get
            {
                if (parentComponent != null)
                {
                    if (isVisible)
                        return parentComponent.IsVisible;

                    return false;
                }

                return isVisible;
            }

            set
            {
                isVisible = value;
                OnVisibleChanged();
            }
        }

        public object UserData
        {
            get { return userData; }
            set { userData = value; }
        }

        public float Opacity
        {
            get { return enabledColor.a / 255f; }
            set
            {
                float clamped = Mathf.Clamp(value, 0f, 1f);

                if (value != Opacity)
                {
                    enabledColor.a = (byte)(clamped * 255f);
                    OnOpacityChanged();
                }

            }
        }

        public UIComponent Parent
        {
            get{ return parentComponent; }

            set
            {
                parentComponent = value;
                OnParentChanged();
            }
        }

        public int ChildCount
        {
            get
            {
                if (childComponents == null)
                    return 0;

                return childComponents.Count;
            }
        }

        public T AddUIComponent<T>() where T : UIComponent
        {
            GameObject obj = new GameObject(typeof(T).Name);
            obj.transform.parent = transform;
            obj.layer = base.gameObject.layer;

            Vector2 vec = size * pixelsToUnits() * 0.5f;
            obj.transform.localPosition = new Vector3(vec.x, vec.y, 0f);

            T comp = obj.AddComponent<T>();
            comp.parentComponent = this;

            AddUIComponent(comp);

            return comp;
        }

        public UIComponent AddUIComponent(Type type)
        {
            return new UIComponent();
        }

        private void AddUIComponent(UIComponent component)
        {
            if (!childComponents.Contains(component))
            {
                childComponents.Add(component);
                component.Parent = this;
                OnComponentAdded();
            }
        }

        public UIComponent Find(string name)
        {
            if (componentName == name)
                return this;

            if (ChildCount > 0)
            {
                foreach (UIComponent component in childComponents)
                    if (component.componentName == name)
                        return component;
            }

            return null;
        }

        public UIComponent Find(Type type)
        {
            if (GetType() == type)
                return this;

            if (ChildCount > 0)
            {
                foreach(UIComponent component in childComponents)
                    if (component.GetType() == type)
                        return component;
            }

            return null;
        }

        public UIComponent Find(string name, Type type)
        {
            if (componentName == name && type.IsAssignableFrom(GetType()))
                return this;

            if (ChildCount > 0)
            {
                foreach(UIComponent component in childComponents)
                    if (component.componentName == name && type.IsAssignableFrom(GetType()))
                        return component;
            }

            return null;
        }

        public T Find<T>(string name) where T : UIComponent
        {
            return Find(name, typeof(T)) as T;
        }

        public T Find<T>(Type type) where T : UIComponent
        {
            return Find(typeof(T)) as T;
        }

        public bool IsInteractible
        {
            get { return isInteractible; }
            set { isInteractible = value; }
        }

        public void OnEnabledChanged()
        {

        }

        public void OnVisibleChanged()
        {

        }

        public void OnOpacityChanged()
        {

        }

        public void OnParentChanged()
        {

        }

        public void OnComponentAdded()
        {

        }

        protected float pixelsToUnits()
        {
            return 0f;
        }
    }
}

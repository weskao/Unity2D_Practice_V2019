﻿using UnityEngine;

namespace Extensions
{
    public static class ComponentExtension
    {
        public static void Show(this Component component)
        {
            component.gameObject.SetActive(true);
        }

        public static void Hide(this Component component)
        {
            component.gameObject.SetActive(false);
        }
    }
}
using UnityEngine;

namespace Editor.SceneViewEditor.Source.Interfaces
{
    public interface IWindow
    {
        int Id { get; }
        bool IsDataEmpty { get; }
        bool IsActive { get; set; }
        Transform Transform { get; }
        
        void Display();
        void Close();
    }
}
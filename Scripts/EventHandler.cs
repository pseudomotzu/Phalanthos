using Godot;
using System;

public class EventHandler : Node
{
    [Export]
    readonly NodePath TextLabelPath;
    RichTextLabel textLabel;


    public override void _Ready()
    {
        textLabel = GetNode<RichTextLabel>(TextLabelPath);

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private async void WriteText()
    {

    }
}

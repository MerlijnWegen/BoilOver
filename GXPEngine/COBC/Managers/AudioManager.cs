using System;
using System.Collections.Generic;
using System.IO;
using GXPEngine;

public class AudioManager
{
    Dictionary<string,Sound> _sounds = new Dictionary<string,Sound>();


    public AudioManager()
    {
        //example: AddSound("name", "filename.mp3");
        LoadSounds();
    }

    public void LoadSounds()
    {
        // Get the program's directory path
        string programDir = Directory.GetCurrentDirectory();

        // Define the path to the "sounds" directory as a subdirectory of the program directory
        string soundsDir = Path.Combine(programDir, "SoundFiles");

        // Get an array of all .mp3 files in the directory
        string[] mp3Files = Directory.GetFiles(soundsDir, "*.mp3");

        // Loop through each .mp3 file and add it to the dictionary
        foreach (string mp3File in mp3Files)
        {
            // Get the filename without the path and extension
            string soundName = Path.GetFileNameWithoutExtension(mp3File);

            // Create a new Sound object with the mp3 file path as the argument
            Sound sound = new Sound(mp3File);

            // Add the Sound object to the dictionary with the filename as the key
            _sounds.Add(soundName, sound);
        }
    }


    public void AddSound(string name, String soundFileName)
    {
        _sounds.Add(name, new Sound(soundFileName));
    }
    public void Play(string name)
    {
        try{
            _sounds[name].Play();
        }
        catch
        {
            Console.WriteLine(name + ".mp3 cannot be found, make sure it is in the \"Soundfiles\" folder");
        }
        
    }

}

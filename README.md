bukkitgui
=========

The bukkit GUI project - shortened BukkitGUI - is intended to provide server owners with a easy to use, powerful GUI. Except from clearly displaying server information, it also adds some powerful functions, like a task planner, error logger and solver, plugin manager, . At this moment it does only use 30MB of your RAM, and 0% CPU when your server is running.

    CPU usage may raise when there is a lot of output, like on startup (with many plugins or) reload. 

Features:

    Coloured output, colours definable in settings
    Detailed player list with context menu, quickly apply an action to a player
    Advanced options to launch bukkit, update bukkit, auto update check for bukkit
    Automate your server with the task manager
    Highly precise memory and CPU measurement
    Low on resources, performance can be adjusted in settings
    Error logging, searching for the cause of errors, and if possible, solving of errors
    Tray icon (with support to minimize to tray) and balloon tips
    Sound when something happen can be activated
    Rarely/Never breaks on Minecraft/bukkit update
    Backups (multiple backup definitions possible)
    Install, update, manage plugins from within the GUI 

Use:

  Remote Server Support
  
    The GUI can also show output and send commands to remote servers. The recommended way to use a remote server is by using JSONAPI. In order for this to work, the JSONAPI plugin should be installed on your server.
    
    If you want to start RemoteToolkit using the GUI, do the following: -Set the server type to bukkit -Select the remote toolkit jar file -Enter the following custom switch: "user:pass" without the ""
    Documentation
  
  
  Downloads
  
    Recommended builds can be downloaded from Bukkitdev. If enabled, the GUI will check for updates and download those from bukkitdev after you agreed with the download.
  
  To-Do
  
    Portforwarding (work in progress) 
  
  Known Issues
  
    Sometimes tasks run twice 
  
  compatibility
    operating systems:
    
      Windows XP and higher. If it doesn't work, make shure if you have .net framework 3.5 Download link - .NET 3.5 including all previous versions.
      Linux and Mac aren't supported, however I'm planning a new project which will work on all operating systems. 
  
    servers:
    
      The following minecraft servers are supported:
    
        Bukkit build 800 and higher (lower isn't tested)
        RemoteToolKit
        Vanilla
        Tekkit
        MCPC (set server type to vanilla,generic java, or to bukkit with disabled "retrieve current version on server start")
        CraftBukkit
        Spigot
        Spout 

# SimpleHTTP
Serves a default.htm .gif .jpg .swf or .js file from it's \wwwroot directory.
Included are a blank page, transparent 1x1 pixel gif, white 1x1 pixel jpg, and 1x1 pixel flash file.
You can modify these to be whatever you like, as long as the filename remains default.x

Speeds up surfing by answering the request for blocked content.

Useful with HOSTS files or DNS Redirector software.

Alternate service name, in order to run multiple instances on the same server:

- SimpleHTTP2 v3.0.0.3
- SimpleHTTP3 v3.0.0.3
- SimpleHTTP4 v3.0.0.3

Tested on Windows XP, Server 2003, Vista, Server 2008, Windows 7, Windows 8

A scripted install/uninstall is not included with this software.

This program runs as a service; without any GUI, taskbar, or system tray icon.

<b>Installation:</b>

1) Ensure the Microsoft .NET Framework 4.x is installed
2) Right-click on the .zip file you downloaded, select properties, click the Unblock button (if this button is not present just proceed)
3) Extract the contents of the .zip file (usually to C:\SimpleHTTP)
4) Edit SimpleHTTP.ini to include the IP of your server and path to the default.x files (usually C:\SimpleHTTP\wwwroot)
5) Right-click on _setup_.bat and select "Run as administrator" to install the service (see note at bottom of this text)

View the default .ini file that's included. Anytime you make a change in SimpleHTTP.ini you must restart the service for it to take effect.

<b>Usage:</b>

SimpleHTTP will also serve the following files when requested and found in the path...
/default001.htm through /default005.htm
/default001.gif through /default005.gif
/default001.jpg through /default005.jpg
/default001.swf through /default005.swf

No GUI is displayed, check Task Manager or tcpview to see that it's running.

Note: Under Windows Vista and later OS versions you may receive an error (HRESULT: 0x80131515) when installing the service.
The solution is to save the .zip file, right-click on it, select properties, and click the Unblock button.
Then extract the contents of the .zip file. Right-click on _setup_.bat and select "Run as administrator" and it will work.

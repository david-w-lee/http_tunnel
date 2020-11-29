# HTTP Tunnel



### Summary

* HTTP Tunnel is a way to connect to target host via proxy server.
* Normally, this is used to access websites using HTTPS.
* Since the proxy is merely shuffling bytes between two sides, it has no visibility on the data being transferred.



### Usage

* Install and open up a proxy that support HTTP Tunnel such as Fiddler or Squid.
* Change the host, port in our program and run it. `dotnet run`
* Verify that a tunnel has been created and response came back from the destination.



### Ref

* https://en.wikipedia.org/wiki/HTTP_tunnel
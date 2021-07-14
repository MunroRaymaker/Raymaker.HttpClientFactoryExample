# Introduction 
Demonstrates the use of IHttpClientFactory. 
Wraps the HttpClient in Polly which handles transient errors and adds delay between retries.

# HttpClient
A new HttpClient instance is returned each time CreateClient is called on the IHttpClientFactory. An HttpMessageHandler is created per named client. The factory manages the lifetimes of the HttpMessageHandler instances.

IHttpClientFactory pools the HttpMessageHandler instances created by the factory to reduce resource consumption. An HttpMessageHandler instance may be reused from the pool when creating a new HttpClient instance if its lifetime hasn't expired.

Pooling of handlers is desirable as each handler typically manages its own underlying HTTP connections. Creating more handlers than necessary can result in connection delays. Some handlers also keep connections open indefinitely, which can prevent the handler from reacting to DNS (Domain Name System) changes.

# Refences
Credits goes to https://www.youtube.com/watch?v=9pgvX_Dk0n8&t=31s&ab_channel=NickChapsas. Check his channel for more great tutorials.
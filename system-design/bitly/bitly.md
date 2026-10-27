# Bitly System Design

[← Back to System Design](../system-design.md)

## Overview

Bitly is a URL shortening service that converts long URLs into shorter, more manageable links while maintaining the ability to redirect to the original URLs. The system needs to handle high-volume URL shortening, fast redirection, and analytics tracking. It requires efficient URL generation algorithms, fast lookup mechanisms, and a scalable infrastructure to support millions of URL shortenings and redirects while maintaining low latency and high availability.

## Functional Requirements

users can create a short url from long url
optionally support custom alias
optionally support expiration time
users to be redirected to long url from short url

## Non-Functional Requirements

Low latency on redirects (~200ms)
Scale to support 100M DAU and 1B urls
Ensure uniqueness of codes to avoid collision
CAP Theorem (Consistency, Availability, Partition Tolerance)
Partition Tolerance
High Availability
Eventual Consistency instead of strong consistency
strong read after write consistency is not required

## Back of the Envelope Estimations

Scale
Latency
Storage

## API Endpoints

``` c-sharp
// shorten a url
POST urls -> shortUrl
{
  originalUrl,
  alias?,
  expirationTime?
}

// redirect
GET /{shortUrl} -> Redirect to OriginalUrl

Note: 302 redirect, logging and understand if something broke
301 Redirect -> permanent (cached by browser and DNS)
302 Redirect -> temporary
```

## Object Model

Url

* userId, int
* shortUrl, varchar
* longUrl, varchar
* alias, varchar
* expirationTime, timestamp
* creationTime, timestamp

User

* userId, int
* firstName, varchar
* lastName, varchar
* email, varchar
* password, varchar
* creationTime, timestamp

## System Design Diagram

![Bitly System Design](url-shortener.svg)

[Download Draw.io File](url-shortener.drawio)

## Additional Notes

fast
unique
short (5-7 characters)

prefix of a long url
random number generator
atleast 1B based on requirement, 10^9, which is 10 characters
base62 encoding, 0-9, A-Z, a-z (10+26+26 = 62)
62^6 is 56 B, Collision is still high, Birthday Paradox (2 people in 23 people have 50% chance of same birthday)
check for collision, introduces a read
hash the long url
md5(longUrl) -> hash -> base62(hash) -> First 6 characters
same collision as above
Counter
increment the counter -> base62 (counter)
predictability leading to bad security
product warning
rate limiting
fix using bijective functions, which will have 1:1 mapping
will not require read from DB

Cache
read through cache
LRU cache

Potential use of CDN
helps with geographical caching and reducing latency
downside that redirects would be permanent

100M users, 10^8
10^5 seconds in a day
Requests per second
10^8 / 10^5 = 10^3
Peak per day, multiply by 100
10^5 rps
Generally, EC2 handles 10^3 concurrent rps
Reads need to scale here instead of writes, since not many people will be creating short URL
Data
8+100+8+100+8 = 224 bytes
Add misc and make it 500 bytes
500 bytes * 1B = 500GB
Single DB can easily handle it
In future if needed we can Shard based on shortUrl. In case of 3 DB, DB % 3 to find the right DB

### Video References

[![Bitly System Design Video](https://img.youtube.com/vi/iUU4O1sWtJA/0.jpg)](https://www.youtube.com/watch?v=iUU4O1sWtJA)

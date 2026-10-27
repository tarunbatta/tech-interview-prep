# Instagram System Design

[← Back to System Design](../system-design.md)

## Overview

Instagram is a social media platform focused on photo and video sharing, with features like stories, reels, and direct messaging. The system needs to handle massive media storage, real-time content delivery, social interactions, and personalized content recommendations. It requires efficient content delivery networks, robust caching mechanisms, and a scalable infrastructure to support billions of users and their daily interactions with photos, videos, and social features.

## Functional Requirements

* upload images from mobile client
* allow users to follow users
* generate a feed of images
* 10 mil users

## Non-Functional Requirements

* High Scalability
* High availability
* Eventual Consistency
* High Durability
* Low Latency

## Back of the Envelope Estimations

* Scale
  * 10 mil DAU
  * 10 * 10^6 / 10^5 = 100 (write) requests per sec
  * Read to write ratio, 100:1
  * 10K (read) requests per sec
  * 10100 QPS (queries per second)
* Storage
  * 10^7 *2 (photos per day)* 5 MB (avg size of photo)
  * 10 ^8 MB
  * 100 TB per day
  * 100 TB * 365 = 36.5 PB per year
  * 365 PB for 10 years

## API Endpoints

``` c-sharp
POST /api/upload
Params: file and media metadata
Response Code: 201 created
GET /api/post/{id}
Params: postId
Response Code: 200 success 
GET /api/feed
Params: userId, pagination
Response Code: 200 success 
```

## Object Model

User

* userId, int
* firstName, varchar
* lastName, varchar
* email, varchar
* password, varchar
* location, varchar
* creationTime, timestamp

Post

* postId, int
* userId, int
* caption, varchar
* location, varchar
* creationTime, timestamp

Media

* mediaId, int
* userId, int
* caption, varchar
* location, varchar
* path, varchar
* creationTime, timestamp

Postmedia

* postid, int
* mediaId, int

Follower

* userId, int
* followerId, int

## System Design Diagram

![Instagram System Design](instagram.svg)

[Download Draw.io File](instagram.drawio)

## Additional Notes

### Feed Model

push model for majority of users i.e. when user posts the the feed caches of their followers are updates
pull model for celebrities where users are forced to get the latest posts from celebrities they follow

### Video References

[![Instagram System Design Video](https://img.youtube.com/vi/anM-cr6RqDI/0.jpg)](https://www.youtube.com/watch?v=anM-cr6RqDI)

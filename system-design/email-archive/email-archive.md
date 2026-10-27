# Email Archive System Design

[← Back to System Design](../system-design.md)

## Overview

An email archive system is designed to store, index, and retrieve historical email communications for compliance, legal, and business purposes. The system needs to handle massive email storage, efficient search capabilities, data retention policies, and secure access controls. It requires robust indexing mechanisms, data compression, and compliance features to maintain regulatory requirements while ensuring quick access to archived emails.

## Functional Requirements

## Non-Functional Requirements

## Back of the Envelope Estimations

## API Endpoints

## Object Model

## System Design Diagram

![Email Archive System Design](email-archive.svg)

[Download Draw.io File](email-archive.drawio)

## Additional Notes

### Step-by-Step Interaction Flow Client Action (User Archives)

1. User clicks "Archive" on an email/conversation in the client.

2. Client (Optimistic UI Update):
   - The client immediately moves the email to the "Archive" view locally
   - Provides instant feedback to the user

3. Client (API Request):
   - The client sends an HTTP POST request to the API Gateway:

   ```text
   URL: /api/v1/emails/{emailId}/archive
   Body: { 
     "userId": "...", 
     "clientVersion": "timestamp_or_version_id_of_email_state" 
   }
   ```

   Note: clientVersion helps with optimistic concurrency. It indicates the state the client saw when it initiated the archive.

4. API Gateway:
   - Receives the request
   - Authenticates and authorizes the user
   - Forwards the request to an available API Server

5. API Server:
   - Validates the request
   - Enqueues Archive Event: Instead of directly updating the database, it publishes an ArchiveEmailEvent message to the Message Queue

   ```text
   Topic: user_actions
   Message: { 
     "type": "ArchiveEmail", 
     "emailId": "...", 
     "userId": "...", 
     "clientVersion": "..." 
   }
   ```

   - Sends an HTTP 202 Accepted response back to the client immediately (indicating the request is being processed asynchronously)

6. Action Processor Worker (Consumes from Message Queue):
   - A worker consumes the ArchiveEmailEvent from the user_actions queue
   - Database Read: Fetches the current state of the emailId from the Database (e.g., conversation state, last updated timestamp)
   - Conflict Detection: Compares the clientVersion from the event with the lastUpdated timestamp in the database
   - If clientVersion is older than lastUpdated (conflict detected): This means the email state has changed after the client initiated the archive but before this worker processed it (e.g., a new thread arrived)
   - Conflict Resolution (Archive Wins): The worker proceeds to archive the email anyway. Any new threads for this email will also be considered archived
   - If clientVersion matches or is newer (no conflict): The state is as the client saw it
   - Database Write: Updates the email/conversation record in the Database:
     - Set status = 'ARCHIVED'
     - Update lastUpdated timestamp
   - Cache Invalidation: Invalidates relevant entries in the Cache (e.g., the user's inbox view)
   - Real-time Update (Optional/Important): Publishes an EmailArchived event to the Real-time Communication Service (or a separate internal queue)

7. Real-time Communication Service (if applicable):
   - Receives EmailArchived event
   - Pushes an update to all connected clients (e.g., other devices) for that user:

   ```text
   WebSocket message: { 
     "type": "emailUpdated", 
     "emailId": "...", 
     "newStatus": "archived" 
   }
   ```

8. Other Clients (Listening):
   - If the user has the email open on another device, that client receives the WebSocket update and refreshes its UI to show the email as archived

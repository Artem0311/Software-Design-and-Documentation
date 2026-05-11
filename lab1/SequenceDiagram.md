```mermaid
sequenceDiagram
    autonumber
    actor UserA as Client (User A)
    participant API as Backend API
    participant MsgService as Message Service
    participant DB as Database
    participant Status as Status Service
    actor UserB as Client (User B - Offline)

    UserA->>API: Send Message to User B
    API->>MsgService: Route Message
    MsgService->>DB: Save Message
    DB-->>MsgService: Message Saved (ID)
    MsgService->>Status: Update Status to "Sent"
    Status->>DB: Save "Sent" Status
    MsgService-->>API: Success
    API-->>UserA: 200 OK (Status: Sent)

    Note over MsgService, UserB: User B is currently offline.<br/>Delivery is pending.

    UserB->>API: Connects to network / Opens app
    API->>MsgService: Fetch pending messages
    MsgService-->>UserB: Deliver Message
    UserB->>API: Send Delivery Acknowledgement
    API->>Status: Update Status to "Delivered"
    Status->>DB: Save "Delivered" Status
````

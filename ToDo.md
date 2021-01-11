
API definition
---------------------------------

What's implemented and what's not...

Links are now broken. Here is the [old documentation via the WaybackMachine](https://web.archive.org/web/20140719025807/http://developer.linkedin.com/documents/people).

Check out the README file to understand how you can contribute to these tasks.

### Basis

- [x] APIs are grouped as `<ApiGroup />`
- [x] Extract UrlPathParameters from `<ApiMethod Path="/v1/people/id={MemberToken}" />`
- [x] Declare `<ReturnType />` and `<ApiMethod ReturnType="Person" />`
- [ ] Add SecureUrls in configuration and use `secure-urls=true`

### Profiles
  
- [x] [user profile of the current user](https://developer.linkedin.com/documents/profile-api)
- [x] [user profile by id](https://developer.linkedin.com/documents/profile-api)
- [x] [user profile by url](https://developer.linkedin.com/documents/profile-api) 
- [x] [Connections API](https://developer.linkedin.com/documents/connections-api)
- [x] [Connections API: Finding New Connections](https://developer.linkedin.com/documents/connections-api)
- [ ] [People Search API](https://developer.linkedin.com/documents/people-search-api)
- [ ] [Accessing Out of Network Profiles](https://developer.linkedin.com/documents/accessing-out-network-profiles)
- [x] [Profile original picture](http://stackoverflow.com/questions/6955647/getting-linkedin-profile-picture/6963184)

### Companies

> For the purposes of testing, we've created a special LinkedIn Company Page that is available for all developers to use. This company page doesn't require the authenticated user making the API call to be an administrator of the company: https://www.linkedin.com/company/devtestco (Company ID 2414183) 

- Get operations
    - [x] Companies  
    - [x] Get Company shares
- [Post Company Share](https://developer.linkedin.com/creating-company-shares)  
    - [x] Post a new company share
    - [ ] Re-post a share. *you provide an attribution/id value instead of a content block*
    - [ ] check to see if a company has shares enabled
    - [ ] check to see if the current viewer is an administrator for a company (so they can share)
    - [ ] make a single call to fetch a list of all companies the current user is an administrator of (pagination by 10)
- [Historical company statistics](https://developer.linkedin.com/historical-company-statistics)
    - [ ] Fetching Historical Follower Statistics
    - [ ] Fetching Daily Stats
    - [ ] Fetching Historical Status Update Statistics
    - [ ] Fetching Daily Stats
    - [ ] Fetching Statistics for a Specific Company Update
- [Company Statistics](https://developer.linkedin.com/company-statistics)
    - [ ] retrieve statistics for a particular company page
- [Targeting Company Shares](https://developer.linkedin.com/documents/targeting-company-shares)
    - [ ] segment: share-target-reach
    - [ ] segment: share-targets
    - [ ] segment: share-target
    - [ ] segment: code
    - [ ] segment: tvalues
    - [ ] segment: tvalue 
    - [ ] Targeting Minimums
    - [ ] targeting code: geographies
    - [ ] targeting code: company sizes
    - [ ] targeting code: job functions
    - [ ] targeting code: industries
    - [ ] targeting code: seniorities
- Other operations
    - [ ] [Commenting on a Company Share](https://developer.linkedin.com/documents/commenting-and-liking-company-share)
    - [ ] [Liking a Company Share](https://developer.linkedin.com/documents/commenting-and-liking-company-share)
    - [ ] [Comment on Company Updates as Company](https://developer.linkedin.com/documents/commenting-and-liking-company-share)
    - [x] [Company Search](https://developer.linkedin.com/documents/company-search)
    - [ ] [Retrieving a List of Companies Followed](https://developer.linkedin.com/documents/company-follow-and-suggestions)
    - [ ] [Start Following a Company](https://developer.linkedin.com/documents/company-follow-and-suggestions)
    - [ ] [Stop Following a Company](https://developer.linkedin.com/documents/company-follow-and-suggestions)
    - [ ] [Suggested Companies to Follow](https://developer.linkedin.com/documents/company-follow-and-suggestions)

### Groups

#### Group Memberships for a User

- [ ] [Group Posts](https://developer.linkedin.com/documents/groups-api)
- [ ] [Retrieving a Group's Profile Details](https://developer.linkedin.com/documents/groups-api)
- [ ] [Group Memberships for a User](https://developer.linkedin.com/documents/groups-api)
- [ ] [PUT a simple group membership request](https://developer.linkedin.com/documents/groups-api)

####  Retrieving a Group's Discussion Posts

- [ ] [Retrieving a Group's Discussion Posts](https://developer.linkedin.com/documents/groups-api)
- [ ] [Show Latest Discussions for a Group](https://developer.linkedin.com/documents/groups-api)
- [ ] [Show Most Popular Discussions for a Group](https://developer.linkedin.com/documents/groups-api)
- [ ] [Show Posts a User has Started for a Group](https://developer.linkedin.com/documents/groups-api)
- [ ] [Show Posts a User has Participated in for a Group](https://developer.linkedin.com/documents/groups-api)
- [ ] [Show Posts a user is Following for a Group](https://developer.linkedin.com/documents/groups-api)

####  Creating a Group Discussion Post

- [ ] [Creating a Group Discussion Post](https://developer.linkedin.com/documents/groups-api)

####  Interacting with a Discussion Post

- [ ] [Paginate through all Comments of a Post](https://developer.linkedin.com/documents/groups-api)
- [ ] [Like/Unlike a Post](https://developer.linkedin.com/documents/groups-api)
- [ ] [Follow/Unfollow a Post](https://developer.linkedin.com/documents/groups-api)
- [ ] [Flag a Post as a Promotion or Job](https://developer.linkedin.com/documents/groups-api)
- [ ] [Delete a Post or Flag as Inappropriate](https://developer.linkedin.com/documents/groups-api)

####  Interacting with Comments

- [ ] [Comment Resource Full Field List](https://developer.linkedin.com/documents/groups-api)
- [ ] [Add a Comment to a Post](https://developer.linkedin.com/documents/groups-api)
- [ ] [Delete a Comment or Flag as Inappropriate](https://developer.linkedin.com/documents/groups-api)

####  Suggested Groups for a LinkedIn Member

- [ ] [Get Suggested Groups for a User](https://developer.linkedin.com/documents/groups-api)
- [ ] [Remove a Group Suggestion for a User](https://developer.linkedin.com/documents/groups-api)

### Jobs

> Jobs API is a part of our Vetted API Access Program. You must [apply here](https://help.linkedin.com/app/api-dvr) and get LinkedIn's approval before using this API.

- [ ] [Use Field Selectors to retrieve the specific fields](https://developer.linkedin.com/documents/job-lookup-api-and-fields)
- [ ] [Job Fields](https://developer.linkedin.com/documents/job-fields)
- [ ] [Bookmarking a Job](https://developer.linkedin.com/documents/job-bookmarks-and-suggestions)
- [ ] [Deleting a Job Bookmark](https://developer.linkedin.com/documents/job-bookmarks-and-suggestions)
- [ ] [Retrieving a List of a Member’s Suggested Jobs](https://developer.linkedin.com/documents/job-bookmarks-and-suggestions)

#### Search

- [ ] [Job Search API](https://developer.linkedin.com/documents/job-search-api)
- [ ] [Refine your search by providing query parameters, such as keywords, job-title, location, and so on](https://developer.linkedin.com/documents/job-search-api)

#### Posting

- [ ] [Posting a Job](https://developer.linkedin.com/documents/code-sample-posting-job-c)
- [ ] [Posting a Job](https://developer.linkedin.com/documents/posting-job)
- [ ] [Editing a Job](https://developer.linkedin.com/documents/editing-job)
- [ ] [Renewing a Job](https://developer.linkedin.com/documents/renewing-job)
- [ ] [Closing a Job](https://developer.linkedin.com/documents/closing-job)

### Share and Social Stream

- [ ] [Posting Shares](https://developer.linkedin.com/documents/share-api)
- [ ] [retrieve the member's updates](https://developer.linkedin.com/documents/get-network-updates-and-statistics-api)
- [ ] [retrieve the member's first-degree connection updates](https://developer.linkedin.com/documents/get-network-updates-and-statistics-api)
- [ ] [retrieve updates from 1st degree connections of the authenticated user](https://developer.linkedin.com/documents/get-network-updates-and-statistics-api)
- [ ] [Network Update Types](https://developer.linkedin.com/documents/get-network-updates-and-statistics-api)
- [ ] [how many connections they have one and two degrees away](https://developer.linkedin.com/documents/get-network-updates-and-statistics-api)

- [ ] [Getting More Comments and Likes on a Specific Network Update](https://developer.linkedin.com/documents/commenting-reading-comments-and-likes-network-updates)
- [ ] [Use a similar syntax to retrive the complete list of people who liked an update](https://developer.linkedin.com/documents/commenting-reading-comments-and-likes-network-updates)
- [ ] [Adding a Comment or Like to a Network Update](https://developer.linkedin.com/documents/commenting-reading-comments-and-likes-network-updates)
- [ ] [Network Update Comment XML POST Body](https://developer.linkedin.com/documents/commenting-reading-comments-and-likes-network-updates)

- [ ] [Post Network Update](https://developer.linkedin.com/documents/post-network-update)

### Communications

- [ ] [Invitation ](https://developer.linkedin.com/documents/invitation-api)

- [ ] [mailbox](https://developer.linkedin.com/documents/messaging-between-connections-api)

Code generation
---------------------------------

- [x] Setup .tt file and generation class 
- [x] Generate API groups 
- [x] Generate API group
- [x] Generate basic return types 
- [x] Generate basic methods
- [x] Support 1 level of field selectors `/v1/people/id=12345:(first-name,last-name)`
- [x] Support all levels of field selectors
- [x] Collections of ResultType  
- [x] Fix generation of sub-fields (ex: location:(name))  
- [ ] Support entity selectors `/v1/people::(~,id=123456,id=456789):(first-name)` ([ref](https://developer.linkedin.com/documents/field-selectors)) 
    - [ ] API Definition syntax
    - [ ] Collection of selectors
    - [ ] Generate API method

API other items
---------------------------------

- [x] Profile: Some members have profiles in multiple languages. To specify the language you prefer, pass an Accept-Language HTTP header.
- [x] Pagination: start=0, count=500
- [ ] `/v1/companies?is-company-admin=true`

For developers
---------------------------------

- [x] Success HTTP codes: 200, 201
- [x] Error HTTP codes: 400, 401, 403, 404, 500
- [x] async/await pattern Windows Phone 8ish and RT
- [ ] Async pattern by callbacks for Silverlight and Windows Phone 7ish
- [x] Nuget package
- [ ] Full exception documentation to ensure your apps won't crash on unexpected exception types
- [ ] WinRT build



# Published
Published in Azure
https://blogfortest1.azurewebsites.net/ 

# Editor User
user: useradmin@gmail.com
pass: 123456

# Witer User
user: user@gmail.com
pass: 123456

Home page shows latest and appoved posts.
https://blogfortest1.azurewebsites.net/Account/Login login page
https://blogfortest1.azurewebsites.net/Posts/AdminView shows pending posts (only for editor user)
https://blogfortest1.azurewebsites.net/Posts/ViewPost/id  shows post content (for all users, only editor can appove or reject)
https://blogfortest1.azurewebsites.net/Posts/CreatePost  for authenticated users


# API Endpoints
For pending: GET https://blogfortest1.azurewebsites.net/api/posts/pending
To approve: GET https://blogfortest1.azurewebsites.net/api/posts/approve/id
To reject: GET https://blogfortest1.azurewebsites.net/api/posts/reject/id

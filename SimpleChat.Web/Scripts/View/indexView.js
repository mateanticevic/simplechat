var token = localStorage.getItem("token");

if (token === null)
{
    window.location.href = '/home/login';
}
else
{
    window.location.href = '/chat';
}
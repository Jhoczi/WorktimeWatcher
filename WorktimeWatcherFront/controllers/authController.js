import fetch from 'node-fetch';
import https from "https";

const httpsAgent = new https.Agent({
    rejectUnauthorized: false,
});
const URI = "https://localhost:7003/api/Login";

const Login = async (req,res) => {
    let responseData = await postLoginData(URI, {
        login: req.body.email,
        password: req.body.password
    });
    if (!responseData.token)
    {
        await res.redirect('/login-failed');
    }
    else
    {
        req.session.user = req.body.email;
        req.session.token = responseData.token;
        await res.redirect('/dashboard');
    }
};

const Logout = (req,res) => {
    req.session.destroy(function(err){
        if (err)
        {
            console.log(err);
            res.send("Error");
        }
        else
        {
            res.render('index',{title:"Express", logout: "logout Successfully"})
        }
    });
};

async function postLoginData(url = '', data = {})
{
    const response = await fetch(url, {
        method:'POST',
        mode:'cors',
        cache:'no-cache',
        credentials:'same-origin',
        headers: {
            'Content-Type':'application/json'
        },
        redirect:'follow',
        referrerPolicy:'no-referrer',
        body: JSON.stringify(data),
        agent: httpsAgent
    });
    return response.json();
}

export default {Login, Logout};
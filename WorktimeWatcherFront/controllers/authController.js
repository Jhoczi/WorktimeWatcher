
const credential = {
    email:"admin@gmail.com",
    password:"admin123"
}

const Login = async (req,res) => {
    //if (await authWithDatabase(req.body.email,req.body.password))
    if (req.body.email === credential.email && req.body.password === credential.password)
    {
        req.session.user = req.body.email;
        //registerUserActivity(req.session.user);
        res.redirect('/dashboard');
    } else {
        res.redirect('/login-failed');
        //res.end("Invalid Username or password");
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

module.exports = {Login, Logout};
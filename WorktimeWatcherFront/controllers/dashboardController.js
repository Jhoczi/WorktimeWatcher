const Dashboard = (req,res) => {
    if (req.session.user)
    {
        res.render('dashboard',{user: req.session.user})
    }
    else
    {
        res.send("Unauthorized user");
    }
};

const LoginPage = (req,res) => {
    res.render('login-failed');
}
export default {Dashboard,LoginPage};
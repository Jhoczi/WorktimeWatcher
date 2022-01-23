const express = require('express');
const router = express.Router();
const authController = require('../controllers/authController');
const dashboardController = require('../controllers/dashboardController');

/* GET home page. */
router.get('/', function(req, res, next) {
    res.render('index', { title: 'Worktime Watcher' });
});
// route for dashboard
router.get('/dashboard',dashboardController.Dashboard);
// login user
router.post('/login',authController.Login);
// logout user
router.get('/logout', authController.Logout);
router.get('/login-failed/',dashboardController.LoginPage);

module.exports = router;
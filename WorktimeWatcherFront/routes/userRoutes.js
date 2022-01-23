import express from "express";
import authController from "../controllers/authController.js";
import dashboardController from "../controllers/dashboardController.js";

const router = express.Router();
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
router.get('/login-failed',dashboardController.LoginPage);

export default router;
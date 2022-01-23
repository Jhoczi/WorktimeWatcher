import createError from "http-errors";
import express from "express";
import path from "path";
import cookieParser from "cookie-parser";
import logger from "morgan";
import session from "express-session";
import {v4 as uuidv4} from "uuid";
import indexRouter from "./routes/userRoutes.js";

const app = express();

// view engine setup
app.set('views', path.join('views'));
app.set('view engine', 'ejs');

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({extended: false}));
app.use(cookieParser());
app.use(express.static(path.join('public')));

app.use(session({
    secret: uuidv4(),
    resave: false,
    saveUninitialized: true
}));

app.use('/', indexRouter);
app.use('/dashboard', indexRouter);

// catch 404 and forward to error handler
app.use(function (req, res, next) {
    next(createError(404));
});

// error handler
app.use(function (err, req, res, next) {
    // set locals, only providing error in development
    res.locals.message = err.message;
    res.locals.error = req.app.get('env') === 'development' ? err : {};

    // render the error page
    res.status(err.status || 500);
    res.render('error');
});

export default app;

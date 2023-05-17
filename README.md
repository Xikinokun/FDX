# FDX

Configured docker compose up in root folder that launch angular app, web api, console app, db, rabbitmq.
But it contains issues with order of launching services, so you will see some exceptions related with rabbitmq
(web api and console start earlier than rabbitmq, but when rabbitmq has started, everything is ok)
also some issue with nginx, need to launch it with http://localhost:4200, but http://localhost:4200/sms throw 404 :(
Web Api can be reached with http://localhost:5000/swagger/index.html

App was built with Clear Architecture, DDD, CQRS(separate commands and queries for more perfomance on demand)
Angular app doesn't have styling(not good in it)
Code can be improved with: maybe replace SmsConsumer into Consumer console app, and handle all logic there(related with consume, like several events messages). 
Code contains minor duplication because of lack of the time. For instance - logger can be moved to extension method to reuse it.

Thanks, it was good test task, i really enjoyed to do this. i learned a lot of things related with docker when do test task.
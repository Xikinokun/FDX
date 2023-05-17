# FDX

Configured docker compose up in root folder that launch angular app, web api, console app, db, rabbitmq. <br>
But it contains issues with order of launching services, so you will see some exceptions related with rabbitmq <br>
(web api and console start earlier than rabbitmq, but when rabbitmq has started, everything is ok) <br>
also some issue with nginx, need to launch it with http://localhost:4200, but http://localhost:4200/sms throw 404 :( <br>
Web Api can be reached with http://localhost:5000/swagger/index.html <br>
<br>
App was built with Clear Architecture, DDD, CQRS(separate commands and queries for more perfomance on demand) <br>
Angular app doesn't have styling(not good in it) <br>
Code can be improved with: maybe replace SmsConsumer into Consumer console app, and handle all logic there(related with consume, like several events messages). <br>
Would be great to handle optimistic concurrency in ef core with timestamp, apply retry policy(exponential backoff) for messages (transient exceptions for instance) and exactly one message <br>
Also, added test app, can be used with NSubstitute(to check the flow) and IFixture(for dumb data) for each apps(API, Consumer, Application, Infrastucture) <br>
Code contains minor duplication because of lack of the time. For instance - logger can be moved to extension method to reuse it. <br>
<br>
Thanks, it was good test task, i really enjoyed to do this. i learned a lot of things related with docker, rabbitmq(massTransit) when do test task.

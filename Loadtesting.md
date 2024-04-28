It seems as if, on my laptop, that MYSQL breaks at around 147 concurrent users, and this is due to the MySQL server timing out. 

Therefore, it may actually make more sense to load in all of these tables of languages on startup, and have them in memory. In this case, this may
make everything go a lot more smoothly

Response time becomes unacceptable at around 40 concurrent users, with a 8800ms (8.8 second) response time 
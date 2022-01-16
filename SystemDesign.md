# System Design

## Selection of database used for this problem

We can use simple Relational database(sql) and we can put the images (restaurant, logos, dishes) in s3 bucket on amazon. 

## Tables
1- Customer (customer_id, name, Address, Phone_number) => PK : customer_id \

2- Order (creation_time, order_id, meal_id, restaurant_id, customer_id, distance, status) => PK : order_id,meal_id \

3- Restaurant (restaurant_id, name, cooking_slots) => PK: restaurant_id \

4- meal (meal_id, restaurant_id, description, image ) => PK: meal_id \

5- FutureOrder (creation_time, order_id, meal_id, restaurant_id, customer_id, distance, time_of_delivery, isRecurring) => PK: order_id, meal_id

## CRUD Flow

1- We can maintain a queue for each restaurant if the order is normal it will trigger one function which will enqueue the order to the queue.\

2- If the order is future order it will be saved in FutureOrder table.\

3- The time when it will be placed in queue will be calculated by using algorithm written in first question so that it will be delivered on time. We can querry the orders which are 2.5 hours from current time then we will check the estimate time using our algo every 5 minutes if the estimate time seems okay then we will push it to the queue. \

4- If isRecurring in FutureOrder tabel is true Above process will be repeated daily.

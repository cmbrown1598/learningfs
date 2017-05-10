module Movement

[<Measure>] type miles

[<Measure>] type hour

[<Measure>] type mph = miles / hour

let averageSpeed distanceTraveled timeTaken = 
    distanceTraveled / timeTaken

let distance speed timeTaken = 
    speed * timeTaken






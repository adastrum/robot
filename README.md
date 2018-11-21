# Robot Cleaner
Main classes are **Robot** and **Terminal**.

**Robot** does the cleaning and keeps the track of visited places.

**Terminal** deals with the parsing of the input and formatting of the output.

*IDataReader*, *IDataWriter* are there for the cases with different input-output sources (file, web-service etc).

The solution is meant to be simple, but in reald world more complex scenario I would also introduce Infrastructure (implementation of IDataReader, IDataWriter and so) and Application (application service for processing robot workflow) layers.

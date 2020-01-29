# Hamming Neural Network
This is a simple implementation that shows how the Hamming neural network works.
## Features
The program is based on the following neural network scheme:

Letters of the Latin alphabet, positioned on 
images of the BMP format with the size of 200x200 pixels, used as training and test examples. 

There are 3 ways to get image for network test: 
- By noise generator (black and with inversion).
- By drawing test image by hand.
- By loading external image (for example, photo).

## Tests
There are limit values of noise that the network can handle correctly.
For a black noise generator, this value is in the range of 60-70%, and for a generator with inversion, it's approximately 49%.

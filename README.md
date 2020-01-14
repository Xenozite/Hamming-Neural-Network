# Hamming Neural Network
This is a simple implementation that shows how the Hamming neural network works.
![alt text](https://cdn1.savepice.ru/uploads/2020/1/15/8f1886794da4402dc3e18f013df8e644-full.png)
## Features
The program is based on the following neural network scheme:
![alt text](https://cdn1.savepice.ru/uploads/2020/1/15/4f1d656762f318568ebc4202ba4d5ec1-full.png)

Letters of the Latin alphabet, positioned on 
images of the BMP format with the size of 200x200 pixels, used as training and test examples. 

There are 3 ways to get image for network test: 
- By noise generator (black and with inversion).
- By drawing test image by hand.
- By loading external image (for example, photo).

## Tests
1) Noise with 30% inversion. 
![alt text](https://cdn1.savepice.ru/uploads/2020/1/15/0899a12c2753b1fe3205c2280ccb81bb-full.png)

2) Hand-drawed symbol.
![alt text](https://cdn1.savepice.ru/uploads/2020/1/15/d52af4c9445e58a1a6209338e4f53813-full.png)

3) Photo of symbol, drawed on paper.
![alt text](https://cdn1.savepice.ru/uploads/2020/1/15/7a859c684907b56c2929f9aa8963aed0-full.png)

There are limit values of noise that the network can handle correctly.
For a black noise generator, this value is in the range of 60-70%, and for a generator with inversion, it's approximately 49%.

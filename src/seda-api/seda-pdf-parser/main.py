import os
from pdf2image import convert_from_path
import pytesseract
import sys

def parsePdf(filePath: str):
    images = convert_from_path(filePath)
    if len(images) == 0:
        return ""

    images[0].save(f"./title_page.jpg","JPEG")
    result = pytesseract.image_to_string("./title_page.jpg")
    
    os.remove("./title_page.jpg")
    os.remove(filePath)

    return result

if __name__=='__main__':
    if len(sys.argv) != 2:
        exit(1)
    
    print(parsePdf(sys.argv[1]))
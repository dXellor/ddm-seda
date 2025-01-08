from fastapi import FastAPI, File, UploadFile
import os
from pdf2image import convert_from_path
import pytesseract

def parsePdf(contents: bytes):
    with open('temp.pdf', 'wb') as f:
            f.write(contents)

    images = convert_from_path("temp.pdf")
    if len(images) == 0:
        return ""

    images[0].save(f"./title_page.jpg","JPEG")
    result = pytesseract.image_to_string("./title_page.jpg")
    
    os.remove("./title_page.jpg")
    os.remove("./temp.pdf")

    return result

app = FastAPI()

@app.post("/upload")
def upload(file: UploadFile = File(...)):
    contents = file.file.read()
    return {"message": parsePdf(contents)}

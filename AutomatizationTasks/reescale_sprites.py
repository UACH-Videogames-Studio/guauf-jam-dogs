from PIL import Image
import os
import math

def resize_to_nearest_32(image_path):
    # Cargar la imagen
    img = Image.open(image_path)
    original_width, original_height = img.size
    
    # Encontrar el tamaño más cercano múltiplo de 32
    new_width = math.ceil(original_width / 32) * 32
    new_height = math.ceil(original_height / 32) * 32

    # Calcular el factor de escalado para ajustar al tamaño más cercano de 32 píxeles
    scale_factor_x = new_width / original_width
    scale_factor_y = new_height / original_height

    # Tomar el factor más pequeño para mantener la proporción
    scale_factor = min(scale_factor_x, scale_factor_y)

    # Reescalar la imagen sin interpolación (modo NEAREST)
    scaled_width = int(original_width * scale_factor)
    scaled_height = int(original_height * scale_factor)
    resized_img = img.resize((scaled_width, scaled_height), Image.NEAREST)
    
    # Crear una nueva imagen con el tamaño ajustado, con fondo transparente
    new_img = Image.new('RGBA', (new_width, new_height), (0, 0, 0, 0))
    
    # Pegar la imagen escalada alineada a la esquina inferior izquierda
    x_offset = 0
    y_offset = new_height - scaled_height
    new_img.paste(resized_img, (x_offset, y_offset))

    return new_img

def process_images_in_directory(directory, save_directory):
    # Crear el directorio de guardado si no existe
    if not os.path.exists(save_directory):
        os.makedirs(save_directory)

    for filename in os.listdir(directory):
        if filename.endswith(".png"):
            # Generar la ruta completa del archivo
            image_path = os.path.join(directory, filename)
            
            # Redimensionar la imagen
            resized_image = resize_to_nearest_32(image_path)
            
            # Guardar la imagen con el sufijo _32_bits en el directorio de guardado
            new_filename = filename.replace(".png", "_32_bits.png")
            new_image_path = os.path.join(save_directory, new_filename)
            resized_image.save(new_image_path)
            print(f"Imagen guardada: {new_image_path}")

if __name__ == "__main__":
    # Define el directorio donde están los sprites PNG
    directory = "./sprites"
    
    # Define el directorio donde se guardarán las imágenes procesadas
    save_directory = "./sprites_32_bits"
    
    # Procesar las imágenes del directorio
    process_images_in_directory(directory, save_directory)

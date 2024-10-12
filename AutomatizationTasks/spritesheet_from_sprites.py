from PIL import Image
import os

def create_spritesheet(directory, save_path, sprite_height=32, max_sprites_per_row=4, padding=32):
    # Obtener la lista de archivos PNG en el directorio
    sprites = [f for f in os.listdir(directory) if f.endswith('.png')]
    
    if not sprites:
        print("No se encontraron sprites en el directorio.")
        return

    # Variables para controlar el tamaño del spritesheet y la colocación de los sprites
    total_width = 0
    total_height = sprite_height + padding  # Consideramos el padding para la altura
    current_row_width = 0
    row_height = sprite_height
    sprite_positions = []

    # Calcular el ancho total del spritesheet y las posiciones de cada sprite
    for index, sprite_name in enumerate(sprites):
        sprite_path = os.path.join(directory, sprite_name)
        sprite = Image.open(sprite_path)
        sprite_width, sprite_height = sprite.size

        # Si la fila actual se llena con 4 sprites, iniciar una nueva fila
        if len(sprite_positions) % max_sprites_per_row == 0 and len(sprite_positions) > 0:
            total_height += row_height + padding  # Incrementar la altura total considerando el padding
            current_row_width = 0  # Reiniciar el ancho actual de la fila

        # Guardar la posición donde se colocará este sprite (con el padding)
        sprite_positions.append((current_row_width + padding, total_height - row_height))
        current_row_width += sprite_width + padding  # Incrementar el ancho actual de la fila con el padding
        total_width = max(total_width, current_row_width + padding)  # Actualizar el ancho total del spritesheet

    # Crear una nueva imagen para el spritesheet con el ancho y alto calculados
    spritesheet = Image.new('RGBA', (total_width, total_height), (0, 0, 0, 0))

    # Pegar cada sprite en el spritesheet en la posición calculada
    for index, sprite_name in enumerate(sprites):
        sprite_path = os.path.join(directory, sprite_name)
        sprite = Image.open(sprite_path)
        position = sprite_positions[index]
        spritesheet.paste(sprite, position)

    # Guardar el spritesheet
    spritesheet.save(save_path)
    print(f"Spritesheet guardado en: {save_path}")

if __name__ == "__main__":
    # Define el directorio donde están los sprites PNG
    directory = "./sprites"
    
    # Define el nombre y ubicación del spritesheet resultante
    save_path = "./spritesheet.png"
    
    # Crear el spritesheet con separación de 32 píxeles
    create_spritesheet(directory, save_path, sprite_height=32, max_sprites_per_row=4, padding=32)

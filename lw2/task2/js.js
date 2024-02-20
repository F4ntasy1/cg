const PIXEL_SIZE = 8

function getCanvasContext() {
    const canvas = document.getElementById('canvas')
    return canvas?.getContext('2d')
}

function drawPixel(context, x, y) {
    const colorInput = document.getElementById('color')
    context.fillStyle = colorInput.value
    context.fillRect(x, y, PIXEL_SIZE, PIXEL_SIZE)
}

function onChangeFile() {
    const input = document.getElementById('file').files[0]
    const image = document.getElementById('div-image')

    input.value = ''

    const reader = new FileReader()
    reader.readAsDataURL(input)

    reader.onload = () => {
        image.style['background-image'] = `url(${reader.result})`
    }
    reader.onerror = () => {
        alert('Не удалось загрузить файл')
    }
}

function onClickButtonNew() {
    const input = document.getElementById('file')
    input.value = ''
    const image = document.getElementById('div-image')
    image.style['background-image'] = 'none'

    const canvas = document.getElementById('canvas')
    const context = getCanvasContext()
    context.clearRect(0, 0, canvas.width, canvas.height)
}

function onClickButtonOpen() {
    const input = document.getElementById('file')
    input.click()
}

function onClickButtonSave(format) {
    const image = document.getElementById('div-image') 

    html2canvas(image).then(function(canvas) {
        canvas.toBlob(function(blob) {
            saveAs(blob, `image.${format}`)
        });
    });
}

function onMouseMoveHandler(context, e) {
    drawPixel(context, e.clientX - 12, e.clientY - 69)
}

function main() {
    const context = getCanvasContext()

    function onMouseMove(e) {
        onMouseMoveHandler(context, e)
    }

    document.addEventListener('mousedown', () => {
        document.addEventListener('mousemove', onMouseMove)
    })
    document.addEventListener('mouseup', () => {
        document.removeEventListener('mousemove', onMouseMove)
    })
}

main()
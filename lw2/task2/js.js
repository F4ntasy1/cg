const LINE_SIZE = 5
const X_OFFSET = 12
const Y_OFFSET = 69

function getCanvasContext() {
    const canvas = document.getElementById('canvas')
    return canvas?.getContext('2d')
}

function drawLine(context, x, y) {
    const colorInput = document.getElementById('color')
    context.strokeStyle = colorInput.value
    context.lineTo(x, y)
    context.stroke()
}

function onChangeFile() {
    const input = document.getElementById('file').files[0]
    const image = document.getElementById('area')

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
    const image = document.getElementById('area')
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
    const image = document.getElementById('area') 

    html2canvas(image).then(function(canvas) {
        canvas.toBlob(function(blob) {
            saveAs(blob, `image.${format}`)
        });
    });
}

function onMouseMoveHandler(context, e) {
    drawLine(context, e.clientX - X_OFFSET, e.clientY - Y_OFFSET)
}

function main() {
    const area = document.getElementById('area')

    const context = getCanvasContext()
    context.lineWidth = LINE_SIZE
    context.lineCap = 'round'

    function onMouseMove(e) {
        onMouseMoveHandler(context, e)
    }

    area.addEventListener('mousedown', (e) => {
        context.beginPath()
        context.moveTo(e.clientX - X_OFFSET, e.clientY - Y_OFFSET)
        area.addEventListener('mousemove', onMouseMove)
    })
    area.addEventListener('mouseup', () => {
        area.removeEventListener('mousemove', onMouseMove)
    })
}

main()
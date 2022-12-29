const reservationRowElements = document.querySelectorAll('tr.reservation-row');

for (let row of reservationRowElements) {
    row.addEventListener('click', handleClick);
    row.addEventListener('dblclick', handleDoubleClick);
}

function handleDoubleClick(e) {
    let rowElement = e.currentTarget;

    if (isSelected(rowElement)) {
        window.location.assign(`${window.location.origin}/Reservations/Details/${rowElement.dataset.id}`);
    }
}

function handleClick(e) {
    let rowElement = e.currentTarget;

    if (!isSelected(rowElement)) {
        rowElement.classList.add('bg-secondary');
        rowElement.classList.add('text-white');

        getSiblings(rowElement).forEach(e => {
            e.classList.remove('bg-secondary');
            e.classList.remove('text-white');
        });
    } 
}

function isSelected(element) {
    return element.classList.contains('bg-secondary') &&
        element.classList.contains('text-white');
}

function getSiblings(e) {
    // for collecting siblings
    let siblings = [];
    // if no parent, return no sibling
    if (!e.parentNode) {
        return siblings;
    }
    // first child of the parent node
    let sibling = e.parentNode.firstChild;

    // collecting siblings
    while (sibling) {
        if (sibling.nodeType === 1 && sibling !== e) {
            siblings.push(sibling);
        }
        sibling = sibling.nextSibling;
    }
    return siblings;
};

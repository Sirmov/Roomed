const allowedData = ['ReservationHolderId', 'ArrivalDate', 'DepartureDate', 'RoomTypeId', 'Adults', 'Teenagers', 'Children'];
const storageKey = 'createReservation';
const createProfileLinkElement = document.getElementById('create-profile-link');
const reservationsForm = document.getElementById('reservation-form');

if (sessionStorage.getItem(storageKey) !== null) {
    let data = JSON.parse(sessionStorage.getItem(storageKey));
    let inputs = document.querySelectorAll('input.form-control');

    inputs.forEach(i => {
        i.value = data[i.id];
    });

    sessionStorage.removeItem(storageKey);
}

createProfileLinkElement.addEventListener('click', (e) => {
    const formData = new FormData(reservationsForm);
    const formEntries = [...formData.entries()];
    let = data = formEntries.reduce((a, [k, v]) => (allowedData.includes(k) ? Object.assign(a, { [k]: v }) : a), {});
    sessionStorage.setItem(storageKey, JSON.stringify(data));
});
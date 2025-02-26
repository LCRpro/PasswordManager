window.isOnline = () => navigator.onLine;

window.addOnlineOfflineListeners = (dotNetHelper) => {
    window.addEventListener("online", () => dotNetHelper.invokeMethodAsync("SetOnlineStatus", true));
    window.addEventListener("offline", () => dotNetHelper.invokeMethodAsync("SetOnlineStatus", false));
};


function showToast() {
    let toastEl = document.getElementById("notificationToast");
    if (!toastEl) return;

    let toast = new bootstrap.Toast(toastEl);
    toast.show();
}

function hideToast() {
    let toastEl = document.getElementById("notificationToast");
    if (!toastEl) return;

    let toast = bootstrap.Toast.getInstance(toastEl);
    if (toast) toast.hide();
}

function showModal(id) {
    let modalEl = document.getElementById(id);
    if (!modalEl) return;

    let modal = new bootstrap.Modal(modalEl);
    modal.show();
}

function hideModal(id) {
    let modalEl = document.getElementById(id);
    if (!modalEl) return;

    let modal = bootstrap.Modal.getInstance(modalEl);
    if (modal) modal.hide();
}

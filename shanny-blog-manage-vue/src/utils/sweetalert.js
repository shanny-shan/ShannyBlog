import Swal from 'sweetalert2'
export const swal = (
  title,
  text,
  icon,
  showConfirmButton = false,
  showCancelButton = false,
  timer = showConfirmButton ? 0 : 2000
) => {
  return Swal.fire({
    title,
    text,
    icon,
    showConfirmButton,
    showCancelButton,
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    timer,
  })
}

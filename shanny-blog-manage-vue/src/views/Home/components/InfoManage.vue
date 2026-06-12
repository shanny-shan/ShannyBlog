<script setup>
import { onMounted, ref, computed } from 'vue'
import InfoDialog from '@/views/_components/dialog/InfoDialog.vue'
import { useAdminStore, useAccountStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const toast = useToast()
const accountStore = useAccountStore()
const adminStore = useAdminStore()

const currentPage = ref(1)
const itemsPerPage = ref(10)

const editInfo = (item) => {
  adminStore.openDialog('info')
  accountStore.userForm = {
    ...item,
    userDetails: { ...item.userDetails },
  }
  adminStore.isEdit = true
}
const deleteInfo = (item) => {
  swal(
    '',
    '',
    `确定删除名为<span class="text-primary font-bold">${item.userId}</span>的账户吗？`,
    'question',
    true,
    true,
  ).then(async (result) => {
    if (result.isConfirmed) {
      const res = await accountStore.deleteUser(item.uuid)
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        await accountStore.getAllUsers()
      } else {
        toast.error(`${res.data.msg}`)
      }
    }
  })
}

const totalPages = computed(() => {
  return Math.ceil(accountStore.users.length / itemsPerPage.value)
})

// 计算当前页显示的条目
const items = computed(() => {
  const startIndex = (currentPage.value - 1) * itemsPerPage.value
  const endIndex = startIndex + itemsPerPage.value
  return accountStore.users.slice(startIndex, endIndex)
})
// 处理页码变化事件
const handlePageChange = (page) => {
  currentPage.value = page
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

onMounted(async () => {
  await accountStore.getAllUsers()
})
</script>
<template>
  <div>
    <!-- <div class="flex justify-end gap-2">
      <button class="btn btn-primary" @click="adminStore.openDialog('info')">
        Add User
      </button>
    </div> -->
    <div class="overflow-x-auto mt-2">
      <table class="table">
        <!-- head -->
        <thead>
          <tr>
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <th>UserId</th>
            <th>nickName</th>
            <th>userName</th>
            <th>birthday</th>
            <th>sex</th>
            <th>mobile</th>
            <th>lastLogin</th>
            <th>other</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>
              <div class="flex items-center gap-3">
                <div class="avatar">
                  <div class="mask mask-squircle h-12 w-12">
                    <img
                      src="@/assets/images/avatar.jpg"
                      alt="Avatar Tailwind CSS Component"
                    />
                  </div>
                </div>
                <div>
                  <div class="font-bold">{{ item.userId }}</div>
                  <div class="text-sm opacity-50">{{ item.status }}</div>
                </div>
              </div>
            </td>
            <td>{{ item.userDetails?.nickname }}</td>
            <td>{{ item.userDetails?.username }}</td>
            <td>{{ item.userDetails?.birthday }}</td>
            <td>{{ item.userDetails?.sex }}</td>
            <td>{{ item.mobile }}</td>
            <td>{{ item.lastLoginTime.substring(0, 10) }}</td>
            <th>
              <div class="flex gap-2">
                <button class="btn btn-ghost btn-xs" @click="editInfo(item)">
                  Edit
                </button>
                <button class="btn btn-ghost btn-xs" @click="deleteInfo(item)">
                  Delete
                </button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="mt-2 md:mt-10 flex justify-center" v-if="totalPages > 1">
      <PaginationComponent
        :current-page="currentPage"
        :total-pages="totalPages"
        :page-range="5"
        @page-change="handlePageChange"
      />
    </div>
  </div>
  <InfoDialog />
</template>
<style lang="scss" scoped></style>

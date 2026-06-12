<script setup>
import AboutDialog from '@/views/_components/dialog/AboutDialog.vue'
import { onMounted, ref, computed } from 'vue'
import { useAdminStore, useAboutStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const toast = useToast()
const adminStore = useAdminStore()
const aboutStore = useAboutStore()

const currentPage = ref(1)
const itemsPerPage = ref(10)

const editAbout = (item) => {
  adminStore.openDialog('about')
  aboutStore.aboutForm = { ...item }
  adminStore.isEdit = true
}
const deleteAbout = (item) => {
  swal(
    '',
    '',
    `确定删除名称为<span class="text-primary font-bold">${item.name}</span>的个人信息吗？`,
    'question',
    true,
    true,
  ).then(async (result) => {
    if (result.isConfirmed) {
      const res = await aboutStore.deleteAbout(item.id)
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        await aboutStore.getAboutList()
      } else {
        toast.error(`${res.data.msg}`)
      }
    }
  })
}

const totalPages = computed(() => {
  return Math.ceil(aboutStore.aboutList.length / itemsPerPage.value)
})

// 计算当前页显示的条目
const items = computed(() => {
  const startIndex = (currentPage.value - 1) * itemsPerPage.value
  const endIndex = startIndex + itemsPerPage.value
  return aboutStore.aboutList.slice(startIndex, endIndex)
})
// 处理页码变化事件
const handlePageChange = (page) => {
  currentPage.value = page
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

onMounted(async () => {
  await aboutStore.getAboutList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button class="btn btn-primary" @click="adminStore.openDialog('about')">
        Add About
      </button>
    </div>
    <div class="overflow-x-auto mt-2">
      <table class="table">
        <thead>
          <tr>
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <th>Name</th>
            <th>Tag</th>
            <th>Introduce</th>
            <th>GitHub</th>
            <!-- <th>Steam</th> -->
            <th>Web</th>
            <!-- <th>Bilibili</th> -->
            <th>IsActive</th>
            <!-- <th>CreateTime</th> -->
            <th>UpdateTime</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.name }}</td>
            <td>
              {{
                item.tag.length > 5 ? item.tag.slice(0, 5) + '...' : item.tag
              }}
            </td>
            <td>
              {{
                item.introduce.length > 10
                  ? item.introduce.slice(0, 10) + '...'
                  : item.introduce
              }}
            </td>
            <td>{{ item.github }}</td>
            <!-- <td>{{ item.steam }}</td> -->
            <td>{{ item.web }}</td>
            <!-- <td>{{ item.biliBili }}</td> -->
            <td>{{ item.isActive }}</td>
            <!-- <td>{{ item.createTime }}</td> -->
            <td>{{ item.updateTime.substring(0, 10) }}</td>
            <th>
              <div class="flex gap-2">
                <button class="btn btn-ghost btn-xs" @click="editAbout(item)">
                  Edit
                </button>
                <button class="btn btn-ghost btn-xs" @click="deleteAbout(item)">
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
  <AboutDialog />
</template>
<style lang="scss" scoped></style>

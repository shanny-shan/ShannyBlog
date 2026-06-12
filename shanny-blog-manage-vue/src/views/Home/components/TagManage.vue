<script setup>
import TagDialog from '@/views/_components/dialog/TagDialog.vue'
import { onMounted, ref, computed } from 'vue'
import { useAdminStore, useTagStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const toast = useToast()
const adminStore = useAdminStore()
const tagStore = useTagStore()

const currentPage = ref(1)
const itemsPerPage = ref(10)

const editTag = (item) => {
  adminStore.openDialog('tag')
  tagStore.tagForm = { ...item }
  adminStore.isEdit = true
}
const deleteTag = (item) => {
  swal(
    '',
    '',
    `确定删除名称为<span class="text-primary font-bold">${item.name}</span>的标签吗？`,
    'question',
    true,
    true,
  ).then(async (result) => {
    if (result.isConfirmed) {
      const res = await tagStore.deleteTag(item.id)
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        await tagStore.getTagList()
      } else {
        toast.error(`${res.data.msg}`)
      }
    }
  })
}

const totalPages = computed(() => {
  return Math.ceil(tagStore.tagList.length / itemsPerPage.value)
})

// 计算当前页显示的条目
const items = computed(() => {
  const startIndex = (currentPage.value - 1) * itemsPerPage.value
  const endIndex = startIndex + itemsPerPage.value
  return tagStore.tagList.slice(startIndex, endIndex)
})
// 处理页码变化事件
const handlePageChange = (page) => {
  currentPage.value = page
  window.scrollTo({ top: 0, behavior: 'smooth' })
}

onMounted(async () => {
  await tagStore.getTagList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button class="btn btn-primary" @click="adminStore.openDialog('tag')">
        Add tag
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
            <th>NameEn</th>
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
            <td>{{ item.nameEn }}</td>
            <th>
              <div class="flex gap-2">
                <button class="btn btn-ghost btn-xs" @click="editTag(item)">
                  Edit
                </button>
                <button class="btn btn-ghost btn-xs" @click="deleteTag(item)">
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
  <TagDialog />
</template>
<style lang="scss" scoped></style>

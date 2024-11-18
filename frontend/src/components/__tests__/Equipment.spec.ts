import { mount } from '@vue/test-utils'
import { describe, it, expect, vi, beforeEach } from 'vitest'
import Equipment from '../Equipment.vue'
import { VDateInput } from 'vuetify/labs/VDateInput'
import { createVuetify } from 'vuetify'
import 'vuetify/styles'
import { flushPromises } from '@vue/test-utils';
import { mdi } from 'vuetify/iconsets/mdi'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

// Mock data for the test
const mockEquipments = [
  {
    id: 1,
    name: 'Drill',
    category: 'Tools',
    status: 'Available',
    location: 'Warehouse 1',
    lastMaintenanceDate: '2023-05-01',
    rentalRate: 20
  },
  {
    id: 2,
    name: 'Hammer',
    category: 'Tools',
    status: 'In Use',
    location: 'Warehouse 2',
    lastMaintenanceDate: '2023-06-15',
    rentalRate: 5
  }
]

const newEquipment = {
  id: 3,
  name: 'Pliers',
  category: 'Tools',
  status: 'In Use',
  location: 'Warehouse 2',
  lastMaintenanceDate: '2023-06-15',
  rentalRate: 5
}

const editedEquipment = {
  id: 2,
  name: 'Hammer',
  category: 'Tools',
  status: 'Available',
  location: 'Warehouse 3',
  lastMaintenanceDate: '2023-06-15',
  rentalRate: 5
}

const mocks = vi.hoisted(() => ({
  headers: vi.fn(),
  getAll: vi.fn(),
  addEquipment: vi.fn(),
  updateEquipment: vi.fn(),
  deleteEquipment: vi.fn()
}))

// Mock the functions that handle the CRUD operations
vi.mock('@/services/equipmentService', () => ({
  ...mocks
}))

const vuetify = createVuetify({
  components: {...components, VDateInput},
  directives,
  icons: {
    defaultSet: 'mdi',
    sets: {
      mdi
    }
  }
})

global.ResizeObserver = require('resize-observer-polyfill')

const actualRender = () => mount(Equipment, {
  props: {},
  global: {
    plugins: [vuetify],
  }
})

describe('Equipment.vue', () => {
  beforeEach(() => {
    vi.resetAllMocks();

    mocks.getAll.mockResolvedValue([...mockEquipments])
  })

  it('renders the equipment table with data', async () => {
    const wrapper = actualRender();

    await flushPromises();

    const rows = await wrapper.findAll('tbody tr')
    expect(rows.length).toBe(mockEquipments.length) // Check if the rows are rendered

    mockEquipments.forEach((equipment, index) => {
      const columns = rows[index].findAll('td')
      expect(columns[0].text()).toBe(equipment.id.toString())
      expect(columns[1].text()).toBe(equipment.name)
      expect(columns[2].text()).toBe(equipment.category)
      expect(columns[3].text()).toBe(equipment.status)
      expect(columns[4].text()).toBe(equipment.location)
      expect(columns[5].text()).toContain(equipment.lastMaintenanceDate)
      expect(columns[6].text()).toBe(equipment.rentalRate.toString())
    })
  })

  it('opens the dialog to add a new item', async () => {
    const wrapper = actualRender();

    const addButton = wrapper.find('button')
    await addButton.trigger('click')

    expect(wrapper.vm.dialog).toBe(true) // Check if the dialog is opened
  })

  it('adds a new item to the equipment table', async () => {
    mocks.addEquipment.mockResolvedValue({...newEquipment})

    const wrapper = actualRender();

    await flushPromises();

    const addButton = wrapper.find('button')
    await addButton.trigger('click')

    wrapper.vm.editedItem = { ...mockEquipments[0], id: 3 }

    await wrapper.vm.save()

    await flushPromises();

    const rows = wrapper.findAll('tbody tr')

    expect(rows.length).toBe(mockEquipments.length + 1) // Check if a new row is added
  })

  it('edits an existing item in the equipment table', async () => {
    mocks.updateEquipment.mockResolvedValue({...editedEquipment})

    const wrapper = actualRender();

    await flushPromises();

    const editButton = wrapper.findAll('.mdi-pencil').at(0)
    await editButton.trigger('click')

    wrapper.vm.editedItem = { ...mockEquipments[0], name: 'Updated Drill' }
    await wrapper.vm.save()

    await flushPromises();

    const name = wrapper.find('tbody tr:first-child td:nth-child(2)')
    expect(name.text()).toBe('Updated Drill') // Check if the first row is updated
  })

  it('deletes an item from the equipment table', async () => {
    mocks.deleteEquipment.mockResolvedValue(1)
    
    const wrapper = actualRender();

    await flushPromises();

    const deleteButton = wrapper.findAll('.mdi-delete').at(0)
    await deleteButton.trigger('click')

    await wrapper.vm.deleteItemConfirm()

    await flushPromises();

    const rows = wrapper.findAll('tbody tr')
    expect(rows.length).toBe(mockEquipments.length - 1) // Check if a row is deleted
  }) 
})

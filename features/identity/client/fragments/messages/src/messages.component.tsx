import React from 'react'

const CommonMessages = ({ messages }) => {
  if (!messages) {
    return null
  }

  return (
    <div style={{ marginTop: 24 }}>
      {messages.map((message) => (
        <div style={{ marginBottom: 16 }} key={message.text}>
          {message.id} {message.type} {message.text} {JSON.stringify(message.context || {})}
        </div>
      ))}
    </div>
  )
}

const FieldsMessages = ({ fields }) => {
  if (!fields) {
    return null
  }

  const fieldsWithMessages = fields.filter((field) => field.messages)

  if (fieldsWithMessages.length === 0) {
    return null
  }

  return (
    <div style={{ marginTop: 24 }}>
      {fieldsWithMessages.map((field) => (
        <div key={field.name}>
          <div style={{ marginBottom: 8 }}>{field.name}</div>
          <CommonMessages messages={field.messages} />
        </div>
      ))}
    </div>
  )
}

export const Messages = ({ messages, fields }: any) => (
  <div>
    <CommonMessages messages={messages} />
    {fields && <FieldsMessages fields={fields} />}
  </div>
)

#!/bin/bash

# Usage: ./create-system-design.sh <system-name>

if [ -z "$1" ]; then
  echo "Usage: $0 <system-name>"
  exit 1
fi

SYSTEM_NAME="$1"
TEMPLATE_DIR="$(dirname "$0")/template"
TARGET_DIR="$(dirname "$0")/../system-design/$SYSTEM_NAME"
SYSTEM_DESIGN_MD="$(dirname "$0")/../system-design/system-design.md"

# Create the target directory
mkdir -p "$TARGET_DIR"

# Copy and rename template files
for file in "$TEMPLATE_DIR"/template.*; do
  ext="${file##*.}"
  target_file="$TARGET_DIR/$SYSTEM_NAME.$ext"
  cp "$file" "$target_file"
  # Replace all occurrences of 'template' (case-insensitive) with the system name in the new files
  sed -i '' "s/template\b/$SYSTEM_NAME/gI" "$target_file"
done

# Prepare display name (title case, replace - and _ with space, capitalize each word)
DISPLAY_NAME=$(echo "$SYSTEM_NAME" | sed -E 's/[-_]/ /g' | awk '{for(i=1;i<=NF;i++){ $i=toupper(substr($i,1,1)) substr($i,2) }}1')

# Prepare the new entry
NEW_ENTRY="[${DISPLAY_NAME}](${SYSTEM_NAME}/${SYSTEM_NAME}.md)"

# Extract everything before the list, the list, and after the list
before_list=$(awk '/^## Systems/ {print; exit} {print}' "$SYSTEM_DESIGN_MD")
list=$(awk '/^## Systems/ {flag=1; next} flag && /^[0-9]+\. / {print}' "$SYSTEM_DESIGN_MD")
after_list=$(awk '/^## Systems/ {flag=1; next} flag && !/^[0-9]+\. / {print}' "$SYSTEM_DESIGN_MD" | tail -n +2)

# Add the new entry (without number)
list=$(echo -e "$list\n$NEW_ENTRY")

# Strip all leading numbers and dots, sort, and renumber
sorted_list=$(echo "$list" | sed 's/^[0-9][0-9]*\. *//' | sort -f | awk '{printf "%d. %s\n", NR, $0}')

# Write back to the file
{
  echo "$before_list"
  echo ""
  echo "$sorted_list"
  echo "$after_list"
} > "$SYSTEM_DESIGN_MD.tmp" && mv "$SYSTEM_DESIGN_MD.tmp" "$SYSTEM_DESIGN_MD"

echo "Created new system design in $TARGET_DIR with base files and updated system-design.md." 